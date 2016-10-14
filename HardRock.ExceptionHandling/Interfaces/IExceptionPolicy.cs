using System;
using System.Collections.Generic;

namespace HardRock.ExceptionHandling.Interfaces
{
    public interface IExceptionPolicy
    {
        List<BaseExceptionHandler> Handlers { get; }
        string Name { get; set; }
        void ExecuteHandlers(Exception exception, IExecutionContext executionContext);
    }
}