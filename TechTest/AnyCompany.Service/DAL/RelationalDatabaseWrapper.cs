using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Service.Cache;
using AnyCompany.Service.Logger;

namespace AnyCompany.Service.DAL
{
    public class RelationalDatabaseWrapper : IDatabaseWrapper
    {
        private readonly IKeyValueCache _cache;
        private readonly string _connectionString;
        private readonly string _rootQueriesDir;
        private readonly ILogger _logger;


        public RelationalDatabaseWrapper(string connectionString, IKeyValueCache cache, string rootQueriesDir, ILogger logger)
        {
            _connectionString = connectionString;
            _cache = cache;
            _rootQueriesDir = rootQueriesDir;
            _logger = logger;
        }
        
        public async Task<IEnumerable<IDictionary<string, object>>> ExecuteSqlFile(string fileName, IDictionary<string, object> args)
        {
            var logPrefix = $"Query {fileName}";
            _logger?.Log($"{logPrefix}: Init");
            
            var commandText = await GetQueryData(fileName);
            var rows = new List<IDictionary<string, object>>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    // Build the command:
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddRange(args.Select((argKvp) => new SqlParameter(argKvp.Key, argKvp.Value)).ToArray());
                    
                    _logger?.Log($"{logPrefix}: SqlCommand: {commandText}");
                    
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        _logger?.Log($"{logPrefix}: Executed. Parsing results.");

                        if (!reader.HasRows || reader.FieldCount == 0)
                        {
                            throw new KeyNotFoundException($"{logPrefix}: NO results returned!");
                        }
                        while (await reader.ReadAsync())
                        {
                            var columns = new Dictionary<string, object>();
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                var columnName = reader.GetName(i);
                                var value = await reader.GetFieldValueAsync<object>(i);
                                columns.Add(columnName, value);
                            }
                            rows.Add(columns);
                        }
                        
                    }
                    
                    connection.Close();
                }
            }

            return rows;
        }
        
        private async Task<string> GetQueryData(string fileName)
        {
            if (_cache != null)
            {
                var queryStr = await _cache.Get(fileName);
                if (queryStr != null)
                {
                    return queryStr;
                }
            }

            // Get the full query file as provided by the configuration
            var fullPath = Path.Combine(_rootQueriesDir, $"{fileName}.sql");
            
            if (!File.Exists(fullPath))
            {
               // Lookup in the current path if not found in the root path:
               var relativePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                   fullPath);
               if (!File.Exists(relativePath))
               {
                    throw new FileNotFoundException($"DatabaseWrapper - Query file not found: {fullPath}. Relative lookup failed as well: {relativePath}");
               }

               fullPath = relativePath;
            }

            byte[] buffer = null;
            using (var fs = File.OpenRead(fullPath))
            {
                buffer =new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length);
            }

            var contents = Encoding.UTF8.GetString(buffer);
            if (_cache != null)
            {
                await _cache.Set(fileName, contents);
            }
            return contents;
        }
    }
}