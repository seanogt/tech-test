namespace AnyCompany.Service.Logger
{
    /// <summary>
    /// A wrapper for any logger, to be used in DI by the whole codebase 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message with the values.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="values"></param>
        void Log(string message, params object[] values);
    }
}