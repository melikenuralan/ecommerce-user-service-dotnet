using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Abstractions.IServices
{
    public interface ILogService
    {
        void Info(string message);
        void Warning(string message);
        void Error(string message, Exception? ex = null);
    }

}
