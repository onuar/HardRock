namespace HardRock.Core.Intercept
{
    public interface ICoreInterceptor
    {
        object Intercept(IMethodInvocation methodInvocation);
    }
}
