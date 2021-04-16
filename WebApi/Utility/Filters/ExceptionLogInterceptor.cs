using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Utility.Filters
{
    public class ExceptionLogInterceptor : ExceptionFilterAttribute, IAsyncExceptionFilter
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var _exceptionService = context.HttpContext.RequestServices.GetService<IExceptionLogService>();

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();

            string errorMessage = context.Exception.ToString();
            _exceptionService.CreateExceptionLog(errorMessage, controller, action);

            Helper.ExceptionLog(errorMessage);

            return base.OnExceptionAsync(context);
        }
    }
}
