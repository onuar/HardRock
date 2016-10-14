using System;
using System.Reflection;

namespace HardRock.Core.Intercept
{
    public interface IMethodInvocation
    {
        object Proxy { get; }

        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        Type[] GenericArguments { get; }

        object ReturnValue { get; set; }

        MethodInfo MethodInvocationTarget { get; }

        Type TargetType { get; }

        void Proceed();
    }
}
