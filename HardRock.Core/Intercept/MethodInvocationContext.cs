namespace HardRock.Core.Intercept
{
    public struct MethodInvocationContext
    {
        public bool BreakInvocation { get; set; }

        public bool InvokeAfterAspects { get; set; }
    }
}
