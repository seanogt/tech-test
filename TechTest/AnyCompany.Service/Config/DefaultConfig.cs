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
        }
        public string DATABASE_URL { get; }
    }
}