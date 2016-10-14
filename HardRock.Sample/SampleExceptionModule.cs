using System;
using System.Collections.Generic;
using HardRock.Core.DependencyInjector;
using HardRock.Core.Module;
using HardRock.ExceptionHandling;

namespace HardRock.Sample
{
    [CustomOrderedModule(1)]
    internal class SampleExceptionModule : IModule
    {
        public void OnApplicationPreStart(IDependecyInjector container)
        {
            //root exception
            var exceptionHandlingConfiguration = new ExceptionHandlingConfiguration();

            // policy 
            var exceptionPolicy = new ExceptionPolicy<SampleBusinessLevelException>() { Name = "Test policy" };
            //handlers in policy
            Dictionary<string, string> extraParameters = new Dictionary<string, string>();
            var exceptionHandler = new SampleBaseExceptionHandler() { Order = 1 };
            exceptionPolicy.Handlers.Add(exceptionHandler);

            exceptionHandlingConfiguration.Policies.Add(exceptionPolicy);

            container.RegisterInstance(exceptionHandlingConfiguration);
        }

        public void OnApplicationStart(IDependecyInjector container)
        {

        }
    }
}