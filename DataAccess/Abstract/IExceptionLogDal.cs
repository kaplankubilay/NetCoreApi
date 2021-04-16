using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IExceptionLogDal
    {
        Task CreateExceptionLog(string errorMessage, string controller, string action);
    }
}
