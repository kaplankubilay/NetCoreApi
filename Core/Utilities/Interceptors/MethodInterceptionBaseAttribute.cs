using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    /// <summary>
    /// Classlarda, Metodlarda, Birden fazla yerde kullanılabilmesini,Inherit edildiği yerde kullanılabilmesi sağlandı.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        /// <summary>
        /// Attribute ün çalışma önceliğini belirleyen property.
        /// </summary>
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {
        }
    }
}
