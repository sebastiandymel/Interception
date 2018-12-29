using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace SEDYInterception
{
    public sealed class MyInterceptionBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.IsDefined(typeof(DoSomethingExtraAttribute)))
            {
                input.Arguments[0] = 99.0;
            }

            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces() => Type.EmptyTypes;
        public bool WillExecute { get; } = true;
    }
}