using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;
using HardRock.Core.DependencyInjector;
using HardRock.Core.Module;

namespace HardRock.Core.Initializer
{
    public class Bootstrapper :
                                IBootstrapperAssemblyLoader,
                                IBootstrapperAssemblyLoaderWithOnStart,
                                IBootstrapperInitializer,
                                IBootstrapperForPreStart,
                                IBootstrapperForStart
    {
        private static readonly Dictionary<string, IModule> LoadedModules = new Dictionary<string, IModule>();
        private readonly List<ComposablePartCatalog> _catalogs;
        private readonly List<CompositionBatch> _compositionBatches;
        private CompositionContainer _compositionContainer;
        private static IDependecyInjector InternalContainer;

        public Bootstrapper()
        {
            _catalogs = new List<ComposablePartCatalog>();
            _compositionBatches = new List<CompositionBatch>();
        }

        public static IDependecyInjector Container
        {
            get
            {
                if (InternalContainer == null)
                {
                    InternalContainer = new CastleDependencyInjector();
                }

                return InternalContainer;
            }
        }

        public static IBootstrapperAssemblyLoader Create()
        {
            return CreateBootstrapper();
        }

        internal static Bootstrapper CreateBootstrapper()
        {
            return new Bootstrapper();
        }

        public IBootstrapperAssemblyLoaderWithOnStart RegisterAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                RegisterAssembly(assembly);
            }

            return this;
        }

        public IBootstrapperAssemblyLoaderWithOnStart RegisterAssembly(Assembly assembly)
        {
            var assemblyCatalog = new AssemblyCatalog(assembly);
            _catalogs.Add(assemblyCatalog);
            return this;
        }

        public IBootstrapperAssemblyLoaderWithOnStart RegisterPath(string path, string searchPattern)
        {
            if (Directory.Exists(path))
            {
                var directoryInfo = new DirectoryInfo(path);
                var directoryCatalog = new DirectoryCatalog(directoryInfo.FullName, searchPattern);
                _catalogs.Add(directoryCatalog);

                var directoryInfos = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);

                foreach (var directory in directoryInfos)
                {
                    directoryCatalog = new DirectoryCatalog(directory.FullName, searchPattern);
                    _catalogs.Add(directoryCatalog);
                }
            }

            return this;
        }

        public IBootstrapperAssemblyLoaderWithOnStart RegisterCatalog(ComposablePartCatalog catalog)
        {
            _catalogs.Add(catalog);
            return this;
        }

        public IBootstrapperAssemblyLoaderWithOnStart RegisterModule<T>() where T : IModule
        {
            var compositionBatch = new CompositionBatch();
            compositionBatch.AddPart(Activator.CreateInstance<T>());

            _compositionBatches.Add(compositionBatch);

            return this;
        }

        public IBootstrapperInitializer InitializeApplication()
        {
            var aggregateCatalog = new AggregateCatalog();

            foreach (var catalog in _catalogs)
            {
                aggregateCatalog.Catalogs.Add(catalog);
            }

            _compositionContainer = new CompositionContainer(aggregateCatalog);

            foreach (var compositionBatch in _compositionBatches)
            {
                _compositionContainer.Compose(compositionBatch);
            }

            IList<IModule> modules = _compositionContainer.GetExportedValues<IModule>().ToList();

            LoadModules(modules);

            return this;
        }

        public IBootstrapperForPreStart InitializeApplicationWithPreStart()
        {
            InitializeApplication();

            ExecutePreStart();

            return this;
        }

        public IBootstrapperForPreStart ExecutePreStart()
        {
            foreach (var module in LoadedModules.Values)
            {
                module.OnApplicationPreStart(Bootstrapper.Container);
            }

            return this;
        }

        public IBootstrapperForStart ExecuteStart()
        {
            foreach (var module in LoadedModules.Values)
            {
                module.OnApplicationStart(Bootstrapper.Container);
            }

            return this;
        }

        private void LoadModules(IList<IModule> modules)
        {
            IEnumerable<IModule> coreModules = SortModulesByAttributeOrder<CoreOrderedModuleAttribute>(modules);
            IEnumerable<IModule> customModules = SortModulesByAttributeOrder<CustomOrderedModuleAttribute>(modules);
            IList<IModule> notOrderedModules =
                modules.Where(
                    m =>
                        !m.GetType().IsDefined(typeof(CoreOrderedModuleAttribute), false) &&
                        !m.GetType().IsDefined(typeof(CustomOrderedModuleAttribute), false))
                    .OrderBy(module => module.GetType().FullName)
                    .ToList();

            var sortedModules = new List<IModule>();
            sortedModules.AddRange(coreModules);
            sortedModules.AddRange(customModules);
            sortedModules.AddRange(notOrderedModules);

            foreach (var module in sortedModules)
            {
                Type moduleType = module.GetType();
                if (!LoadedModules.ContainsKey(module.GetType().FullName) &&
                    !(moduleType.IsAbstract || moduleType.IsInterface))
                {
                    LoadedModules.Add(module.GetType().FullName, module);
                }
            }
        }

        private IEnumerable<IModule> SortModulesByAttributeOrder<T>(IEnumerable<IModule> modules)
            where T : ModuleOrderAttribute
        {
            return (from module in modules
                    let moduleType = module.GetType()
                    where moduleType.IsDefined(typeof(T), false)
                    let moduleAttribute = moduleType.GetCustomAttributes(true).OfType<T>().Single()
                    orderby moduleAttribute.Order ascending
                    select module).ToList();
        }
    }
}