using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using Castle.DynamicProxy;

namespace Interception.Castle
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            // invocation.ReturnValue = invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments);

            if (invocation.ReturnValue is double)
            {
                invocation.ReturnValue = 100.0;
            }

            if (invocation.ReturnValue.GetType().IsAssignableTo<Task>())
            {
                invocation.ReturnValue = InterceptTask((dynamic)invocation.ReturnValue);
            }
        }



        //public void Intercept(IInvocation invocation)
        //{
        //    invocation.Proceed();

        //    // invocation.ReturnValue = invocation.Method.Invoke(invocation.InvocationTarget, invocation.Arguments);

        //    if (invocation.ReturnValue is double)
        //    {
        //        invocation.ReturnValue = 100.0;
        //    }

        //    if (invocation.ReturnValue.GetType().IsAssignableTo<Task>())
        //    {
        //        invocation.ReturnValue = InterceptTask((dynamic)invocation.ReturnValue);
        //    }
        //}

        private async Task<T> InterceptTask<T>(Task<T> task)
        {
            Debug.WriteLine(".......... Intercepted Task<T> call");

            return await task;
        }
    }
}