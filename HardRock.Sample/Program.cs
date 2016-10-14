using System;
using HardRock.Core.Initializer;
using HardRock.Core.Module;

namespace HardRock.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper
                .Create()
                .RegisterModule<HardRockCoreModule>()
                .RegisterModule<SampleModule>()
                .RegisterModule<SampleExceptionModule>()
                .InitializeApplicationWithPreStart()
                .ExecuteStart();


            var business = Bootstrapper.Container.Resolve<ISampleBusiness>();

            business.DoSomething();

            business.ThrowException();
            Console.ReadLine();
        }
    }
}