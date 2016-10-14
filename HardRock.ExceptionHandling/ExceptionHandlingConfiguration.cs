using System.Collections.Generic;
using HardRock.ExceptionHandling.Interfaces;

namespace HardRock.ExceptionHandling
{
    public class ExceptionHandlingConfiguration : IExceptionHandlingConfiguration
    {
        private readonly ExceptionPolicyCollection _policies;
        public ExceptionHandlingConfiguration()
        {
            _policies = new ExceptionPolicyCollection();
        }

        public ExceptionPolicyCollection Policies => _policies;
    }
}