using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
        
        public async Task<IEnumerable<IDictionary<string, object>>> ExecuteSqlFile(string fileName, IEnumerable<object> args)
        {
            var logPrefix = $"Query {fileName}";
            _logger?.Log($"{logPrefix}: Init");
            
            var commandText = await GetQueryData(fileName);
            var rows = new List<IDictionary<string, object>>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
                    
                    _logger?.Log($"{logPrefix}: SqlCommand: ${commandText}");
                    command.Parameters.AddRange(args.ToArray());
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        _logger?.Log($"{logPrefix}: Executed. Parsing results.");
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

            var fullPath = Path.Combine(_rootQueriesDir, fileName, ".sql");
            
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"DatabaseWrapper - Query file not found: {fullPath}");
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