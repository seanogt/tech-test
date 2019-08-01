using System.IO;
using System.Text;

namespace AnyCompany.Service.Logger
{
    public class StdOutLogger : ILogger
    {
        private readonly Stream _stream;

        public StdOutLogger(Stream stream)
        {
            this._stream = stream;
        }

        public void Log(string message, params object[] values)
        {
            var encoded = Encoding.UTF8.GetBytes(message);
            _stream.Write(encoded, 0, encoded.Length);
        }
    }
}