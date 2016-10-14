using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using HardRock.Core.Module;

namespace HardRock.Core.Initializer
{
    public interface IBootstrapperAssemblyLoader
    {
        IBootstrapperAssemblyLoaderWithOnStart RegisterAssemblies(IEnumerable<Assembly> assemblies);

        IBootstrapperAssemblyLoaderWithOnStart RegisterAssembly(Assembly assembly);

        IBootstrapperAssemblyLoaderWithOnStart RegisterPath(string path, string searchPattern);

        IBootstrapperAssemblyLoaderWithOnStart RegisterCatalog(ComposablePartCatalog catalog);

        IBootstrapperAssemblyLoaderWithOnStart RegisterModule<T>() where T : IModule;
    }
}
