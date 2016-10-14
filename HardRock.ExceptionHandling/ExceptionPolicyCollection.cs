using System;
using System.Collections.Generic;
using System.Linq;
using HardRock.ExceptionHandling.Interfaces;

namespace HardRock.ExceptionHandling
{
    public class ExceptionPolicyCollection : List<IExceptionPolicy>
    {
        public IExceptionPolicy GetPolicyByException(Exception exception)
        {
            foreach (var exceptionPolicy in this)
            {
                var policyExceptionType = exceptionPolicy.GetType().GetGenericArguments()[0];
                if (policyExceptionType == exception.GetType())
                {
                    return exceptionPolicy;
                }
            }

            return null;
        }

        public IExceptionPolicy GetPolicyByName(string policyName)
        {
            var policy = this.FirstOrDefault(_ => _.Name == policyName);
            return policy;
        }
    }
}