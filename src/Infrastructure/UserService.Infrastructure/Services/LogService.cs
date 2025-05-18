using Serilog;
using UserService.Application.Abstractions.IServices;

namespace UserService.Infrastructure.Services
{
    public class LogService : ILogService
    {
        public void Info(string message)
        {
            Log.Information(message);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }

        public void Error(string message, Exception? ex = null)
        {
            if (ex == null)
                Log.Error(message);
            else
                Log.Error(ex, message);
        }
    }
}
