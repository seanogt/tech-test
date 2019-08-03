using System.IO;
using System.Text;

namespace AnyCompany.Service.Logger
{
    /// <summary>
    /// Writes logs to a stream.
    /// Very useful for writing test logs to a console, but production logs to a monitored file, external service, etc.
    /// </summary>
    public class StreamLogger : ILogger
    {
        private readonly Stream _stream;

        /// <summary>
        /// Stream should be open for writing.
        /// </summary>
        /// <param name="stream"></param>
        public StreamLogger(Stream stream)
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