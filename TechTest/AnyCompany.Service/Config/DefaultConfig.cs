using System;

namespace AnyCompany.Service.Config
{
    /// <summary>
    /// Default config reader from env var
    /// </summary>
    public class DefaultConfig : IConfig
    {
        public DefaultConfig()
        {
            this.DATABASE_URL = Environment.GetEnvironmentVariable("DATABASE_URL");
            this.ROOT_QUERIES_DIR = Environment.GetEnvironmentVariable("ROOT_QUERIES_DIR");
        }
        public string DATABASE_URL { get; }
        public string ROOT_QUERIES_DIR { get; }
    }
}