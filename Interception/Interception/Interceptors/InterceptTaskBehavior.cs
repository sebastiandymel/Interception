using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace SEDYInterception
{
    public sealed class InterceptTaskBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            {
                IMethodReturn value = getNext()(input, getNext);

                var method = input.MethodBase as MethodInfo;
                var isAsync = input.Target.GetType().GetMethod(method.Name).GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;

                if (isAsync && value.ReturnValue != null && typeof(Task).IsAssignableFrom(method.ReturnType))
                {
                    return input.CreateMethodReturn(InterceptAsync((dynamic) value.ReturnValue), value.Outputs);
                }

                return value;
            }
        }


        private async Task InterceptAsync(Task task)
        {
            Debug.WriteLine(".......... Intercepted Task call");

            await task.ConfigureAwait(false);
        }

        private async Task<T> InterceptAsync<T>(Task<T> task)
        {
            Debug.WriteLine(".......... Intercepted Task<T> call");

            return await task.ConfigureAwait(false);
        }

        public IEnumerable<Type> GetRequiredInterfaces() => Type.EmptyTypes;
        public bool WillExecute { get; } = true;
    }
}