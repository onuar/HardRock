using System;

namespace HardRock.Core.Intercept.Aspect
{
    public abstract class BaseAspectAttribute : Attribute
    {
        static MethodInvocationContext _methodInvocationContextInstance = new MethodInvocationContext
        {
            BreakInvocation = false,
            InvokeAfterAspects = true
        };

        public MethodInvocationContext MethodInvocationContext
        {
            get { return _methodInvocationContextInstance; }
            set { _methodInvocationContextInstance = value; }
        }

        public int Order { get; set; }

        public virtual MethodInvocationContext BeforeMethodInvocation(IMethodInvocation invocation, MethodInvocationContext methodInvocationContext)
        {
            return _methodInvocationContextInstance;
        }

        public virtual void OnMethodInvocationFailed(IMethodInvocation invocation, Exception exception)
        {
            throw exception;
        }

        public virtual void AfterMethodInvocation(IMethodInvocation invocation, MethodInvocationContext methodInvocationContext)
        {
        }
    }
}