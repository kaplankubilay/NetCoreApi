using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfExceptionLogDal : IExceptionLogDal
    {
        public async Task CreateExceptionLog(string errorMessage, string controller, string action)
        {
            using (var context = new NorthwindContext())
            {
                ExceptionLog log = new ExceptionLog(errorMessage, controller, action);

                await context.ExceptionLogs.AddAsync(log);
                context.SaveChanges();
            }
        }
    }
}
