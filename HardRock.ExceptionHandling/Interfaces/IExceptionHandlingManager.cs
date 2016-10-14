using System;

namespace HardRock.ExceptionHandling.Interfaces
{
    public interface IExceptionHandlingManager
    {
        void HandleException(Exception exception, IExecutionContext executionContext, string policyName = null);
    }
}