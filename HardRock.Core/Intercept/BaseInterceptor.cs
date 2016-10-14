using Castle.DynamicProxy;

namespace HardRock.Core.Intercept
{
    public abstract class BaseInterceptor: IBaseInterceptor
    {
        public abstract object Intercept(IMethodInvocation methodInvocation);

        public virtual void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = Intercept(new CastleInvocationAdapter(invocation, invocation.InvocationTarget));
        }
    }
}