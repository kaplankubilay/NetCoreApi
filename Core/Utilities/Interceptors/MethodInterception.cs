using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        /// <summary>
        /// metodun önünde çalış.
        /// </summary>
        /// <param name="invocation"></param>
        protected virtual void OnBefore(IInvocation invocation)
        {

        }

        /// <summary>
        /// metodun arkasından çalış.
        /// </summary>
        /// <param name="invocation"></param>
        protected virtual void OnAfter(IInvocation invocation)
        {

        }

        /// <summary>
        /// Metod hatalıysa çalış.
        /// </summary>
        /// <param name="invocation"></param>
        protected virtual void OnException(IInvocation invocation, System.Exception e)
        {

        }

        /// <summary>
        /// Metod başarılıysa çalış.
        /// </summary>
        /// <param name="invocation"></param>
        protected virtual void OnSuccess(IInvocation invocation)
        {

        }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
