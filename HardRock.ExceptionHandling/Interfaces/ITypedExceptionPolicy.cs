using System;

namespace HardRock.ExceptionHandling.Interfaces
{
    public interface ITypedExceptionPolicy<TException> : IExceptionPolicy
        where TException : Exception
    {
    }
}