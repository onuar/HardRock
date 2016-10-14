using System;
using HardRock.Core.Initializer;
using HardRock.Core.Intercept;
using HardRock.ExceptionHandling.Interfaces;

namespace HardRock.ExceptionHandling.Module
{
    public class ExceptionHandlingInterceptor : BaseInterceptor
    {
        public override object Intercept(IMethodInvocation methodInvocation)
        {
            var exceptionHandlerManager = Bootstrapper.Container.Resolve<IExceptionHandlingManager>();

            try
            {
                methodInvocation.Proceed();
            }
            catch (Exception exception)
            {
                var executionContext = new ExecutionContext(method: methodInvocation.Method, arguments: methodInvocation.Arguments);
                exceptionHandlerManager.HandleException(exception, executionContext, null);
            }

            return methodInvocation.ReturnValue;
        }
    }
}