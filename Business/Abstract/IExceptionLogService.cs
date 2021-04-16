using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IExceptionLogService
    {
        Task CreateExceptionLog(string errorMessage, string controller, string action);
    }
}
