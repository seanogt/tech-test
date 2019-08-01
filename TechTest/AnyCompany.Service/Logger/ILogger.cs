namespace AnyCompany.Service.Logger
{
    public interface ILogger
    {
        void Log(string message, params object[] values);
    }
}