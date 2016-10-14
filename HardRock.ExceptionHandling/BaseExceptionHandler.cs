using System;
using HardRock.ExceptionHandling.Interfaces;

namespace HardRock.ExceptionHandling
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        public int Order { get; set; }

        public virtual Exception HandledException(Exception exception, IExecutionContext executionContext)
        {
            return exception;
        }
    }
}