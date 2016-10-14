using System;
using HardRock.Core;
using HardRock.Core.Intercept;
using HardRock.Core.Intercept.Aspect;

namespace HardRock.Sample
{
    public class SampleAspect : BaseAspectAttribute
    {
        public override void AfterMethodInvocation(IMethodInvocation invocation, MethodInvocationContext methodInvocationContext)
        {
            if (methodInvocationContext.InvokeAfterAspects)
            {
                Console.WriteLine("Sample after aspect");
            }
        }

        public override MethodInvocationContext BeforeMethodInvocation(IMethodInvocation invocation,
            MethodInvocationContext methodInvocationContext)
        {
            Console.WriteLine("Sample before aspect");
            invocation.Proceed();

            return new MethodInvocationContext()
            {
                BreakInvocation = true,
                InvokeAfterAspects = true
            };
        }
    }
}