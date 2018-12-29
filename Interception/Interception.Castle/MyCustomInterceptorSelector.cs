using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Interception.Castle
{
    public class MyCustomInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return interceptors;
        }
    }
}