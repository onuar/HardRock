using HardRock.Core.DependencyInjector;
using HardRock.Core.Intercept;
using HardRock.Core.Module;
using HardRock.ExceptionHandling.Module;

namespace HardRock.Sample
{
    [CustomOrderedModule(2)]
    internal class SampleModule : IModule
    {
        public void OnApplicationPreStart(IDependecyInjector container)
        {
            container.RegisterType<ISampleBusiness, SampleBusiness>(InstanceMode.Transient, false, typeof(AspectRunnerInterceptor), typeof(ExceptionHandlingInterceptor));
        }

        public void OnApplicationStart(IDependecyInjector container)
        {

        }
    }
}