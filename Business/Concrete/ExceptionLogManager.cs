using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class ExceptionLogManager : IExceptionLogService
    {
        private readonly IExceptionLogDal _exceptionLogDal;

        public ExceptionLogManager(IExceptionLogDal exceptionLogDal)
        {
            _exceptionLogDal = exceptionLogDal;
        }

        public async Task CreateExceptionLog(string errorMessage, string controller, string action)
        {
            await _exceptionLogDal.CreateExceptionLog(errorMessage, controller, action);
        }
    }
}
