using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspect.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        //eğer değer girilmez ise 60 dk default olarak verilecek.
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        /// <summary>
        /// Önce Cache e bakılır yoksa Cache e eklenir, Cache te zaten varsa cache ten getirilir.   
        /// </summary>
        /// <param name="invocation"></param>
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            //Metodun parametrelerine göre bir araya getirir. orn: metod(1,"abc","def") formatında.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            //cache te hiç yoksa buraya girmeyecek.Yani cache te varsa girecek.
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }

            //metod çalışır.(cache te yoksa buraya girecek.)
            invocation.Proceed();
            //calışan değeri cache ekler.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
