namespace AnyCompany.Service.Config
{
    public interface IConfig
    {
        /// <summary>
        /// Connection string for the DB
        /// </summary>
        string DATABASE_URL {get;}
        
        /// <summary>
        /// Path to the SQL queries directory 
        /// </summary>
        string ROOT_QUERIES_DIR {get;}

    }
}