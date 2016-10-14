using System;
using HardRock.ExceptionHandling;

namespace HardRock.Sample
{
    internal class SampleBaseExceptionHandler : BaseExceptionHandler
    {
        public override Exception HandledException(Exception exception, IExecutionContext executionContext)
        {
            Console.WriteLine("Exception handled by policy: " + exception.Message);

            return exception;
        }
    }
}