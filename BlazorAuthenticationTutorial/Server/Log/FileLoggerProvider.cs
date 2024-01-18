using Microsoft.Extensions.Logging;
namespace BlazorAuthenticationTutorial.Server.Log
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;

        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }

        public void Dispose()
        {
            // Cleanup logic, if needed
        }
    }
}
