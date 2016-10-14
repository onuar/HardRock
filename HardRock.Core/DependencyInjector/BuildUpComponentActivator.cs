using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;

namespace HardRock.Core.DependencyInjector
{
    internal class BuildUpComponentActivator : DefaultComponentActivator
    {
        public const string InstanceKey = "instance";

        public BuildUpComponentActivator(ComponentModel model, IKernelInternal kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction)
            : base(model, kernel, onCreation, onDestruction)
        {
        }

        protected override object Instantiate(CreationContext context)
        {
            if (!context.HasAdditionalArguments)
            {
                return base.Instantiate(context);
            }

            return context.AdditionalArguments[InstanceKey];
        }
    }
}
