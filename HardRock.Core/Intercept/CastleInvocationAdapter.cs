using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace HardRock.Core.Intercept
{
    public class CastleInvocationAdapter : IMethodInvocation
    {
        private readonly IInvocation _invocation;
        private readonly object _target;

        public CastleInvocationAdapter(IInvocation invocation, object target)
        {
            _invocation = invocation;
            _target = target;
        }

        public object[] Arguments
        {
            get { return _invocation.Arguments; }
        }

        public Type[] GenericArguments
        {
            get { return _invocation.GenericArguments; }
        }

        public MethodInfo Method
        {
            get { return _invocation.Method; }
        }

        public object Proxy
        {
            get { return _invocation.Proxy; }
        }

        public object Target
        {
            get { return _target; }
        }

        public object ReturnValue
        {
            get { return _invocation.ReturnValue; }
            set { _invocation.ReturnValue = value; }
        }

        public MethodInfo MethodInvocationTarget
        {
            get { return _invocation.MethodInvocationTarget; }
        }

        public Type TargetType
        {
            get { return _invocation.TargetType; }
        }

        public void Proceed()
        {
            _invocation.Proceed();
        }
    }
}
