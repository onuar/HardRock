using System.ComponentModel.Composition;
using HardRock.Core.DependencyInjector;

namespace HardRock.Core.Module
{
    [InheritedExport]
    public interface IModule
    {
        void OnApplicationPreStart(IDependecyInjector container);

        void OnApplicationStart(IDependecyInjector container);
    }
}
