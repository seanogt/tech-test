using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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


        public RelationalDatabaseWrapper(string connectionString, IKeyValueCache cache, ILogger logger)
        {
            this._connectionString = connectionString;
            this._cache = cache;
        }
        
        public async Task<IEnumerable<IDictionary<string, object>>> ExecuteSqlFile(string fileName, params object[] args)
        {
            var commandText = await this.GetQueryData(fileName);
            var rows = new List<IDictionary<string, object>>();
            using (var connection = new SqlConnection(this._connectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddRange(args);
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
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
            var queryStr = await _cache.Get(fileName);
            if (queryStr != null)
            {
                return queryStr;
            }

            byte[] buffer = null;
            using (var fs = File.OpenRead(fileName))
            {
                buffer =new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length);
            }

            var contents = Encoding.UTF8.GetString(buffer); 
            await this._cache.Set(fileName, contents);
            return contents;
        }
    }
}