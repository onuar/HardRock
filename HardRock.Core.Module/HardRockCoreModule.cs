using HardRock.Core.DependencyInjector;
using HardRock.Core.Intercept;
using HardRock.ExceptionHandling;
using HardRock.ExceptionHandling.Interfaces;
using HardRock.ExceptionHandling.Module;

namespace HardRock.Core.Module
{
    [CoreOrderedModule(1)]
    public class HardRockCoreModule : IModule
    {
        public void OnApplicationPreStart(IDependecyInjector container)
        {
            container.RegisterType<IExceptionHandlingManager, ExceptionHandlingManager>();
            container.RegisterType<AspectRunnerInterceptor>();
            container.RegisterType<ExceptionHandlingInterceptor>();
        }

        public void OnApplicationStart(IDependecyInjector container)
        {

        }
    }
}