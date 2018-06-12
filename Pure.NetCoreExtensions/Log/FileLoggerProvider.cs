using Microsoft.Extensions.Logging;

namespace Pure.NetCoreExtensions
{
    public class FileLoggerProvider : ILoggerProvider
    {
         
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName);
        }

        public void Dispose()
        {
        }
    }

}