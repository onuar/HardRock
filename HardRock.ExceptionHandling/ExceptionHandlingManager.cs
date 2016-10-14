using System;
using HardRock.Core.Initializer;
using HardRock.ExceptionHandling.Interfaces;

namespace HardRock.ExceptionHandling
{
    public class ExceptionHandlingManager : IExceptionHandlingManager
    {
        public void HandleException(Exception exception, IExecutionContext executionContext, string policyName = null)
        {
            var exceptionConfiguration = Bootstrapper.Container.Resolve(typeof(ExceptionHandlingConfiguration)) as ExceptionHandlingConfiguration;
            var policy = exceptionConfiguration.Policies.GetPolicyByException(exception);
            if (policy == null)
            {
                throw new PolicyNotFoundException();
            }

            policy.ExecuteHandlers(exception, executionContext);
        }
    }
}