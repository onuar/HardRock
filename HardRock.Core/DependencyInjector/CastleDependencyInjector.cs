using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using HardRock.Core.Initializer;
using HardRock.Utility.Extension;

namespace HardRock.Core.DependencyInjector
{
    public class CastleDependencyInjector : IDependecyInjector
    {
        private IWindsorContainer _container;

        public CastleDependencyInjector(CastleDependencyInjector parentContainer)
            : this()
        {
            parentContainer._container.AddChildContainer(_container);
        }

        public CastleDependencyInjector()
        {
            _container = new WindsorContainer();
        }

        public void StartInitialization()
        {
            _container = new WindsorContainer();
            _container.AddFacility<TypedFactoryFacility>();
        }

        public IDependecyInjector RegisterType<TService>(InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
            where TService : class
        {
            return RegisterType(typeof(TService), instanceMode, overridable, interceptors);
        }

        public IDependecyInjector RegisterType(Type serviceType, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
        {
            var componentRegistration = Component.For(serviceType);

            SetLifestyle(componentRegistration, instanceMode);

            if (overridable)
            {
                componentRegistration.IsFallback();
            }

            RegisterInterceptor(interceptors, componentRegistration);

            _container.Register(componentRegistration);
            return this;
        }

        public IDependecyInjector RegisterType<TInterface, TService>(InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
            where TService : TInterface
            where TInterface : class
        {
            RegisterType(typeof(TInterface), typeof(TService), instanceMode, overridable, interceptors);
            return this;
        }

        public IDependecyInjector RegisterType<TInterface, TService>(string name, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
            where TService : TInterface
            where TInterface : class
        {
            RegisterType(typeof(TInterface), typeof(TService), name, instanceMode, overridable, interceptors);
            return this;
        }

        public IDependecyInjector RegisterType(Type @interface, Type service, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
        {
            RegisterType(@interface, service, null, instanceMode, overridable, interceptors);
            return this;
        }

        public IDependecyInjector RegisterType(Type @interface, Type service, string name = null, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
        {
            return RegisterMultiple(service, new List<Type>() { @interface }, name, instanceMode, overridable, interceptors);
        }

        public IDependecyInjector RegisterMultiple(Type service, List<Type> interfaces, string name = null, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
        {
            if (interfaces.IsNullOrEmpty() || interfaces.Count > 4)
            {
                // TODO : add exception key
                throw new DependencyResolverException(string.Empty);
            }

            if (interfaces.Select(type => type.FullName).Distinct().Count() != interfaces.Count())
            {
                // TODO : add exception key
                throw new DependencyResolverException(string.Empty);
            }

            Type interfaceType = interfaces[0];

            Type[] restOfInterfaces = interfaces.Where(type => type != interfaceType).ToArray();

            ComponentRegistration<object> componentRegistration = Component.For(interfaceType).ImplementedBy(service);

            if (!restOfInterfaces.IsNullOrEmpty())
            {
                componentRegistration.Forward(restOfInterfaces);
            }

            SetLifestyle(componentRegistration, instanceMode);

            if (overridable)
            {
                componentRegistration.IsFallback();
            }

            RegisterInterceptor(interceptors, componentRegistration);

            _container.Register(componentRegistration.Named(name));
            return this;
        }

        public IDependecyInjector Configure()
        {
            //todo: do some configuration stuff.
            return this;
        }

        public IDependecyInjector RegisterInstance<TInterface>(TInterface instance, bool overridable = false, params Type[] interceptors)
        {
            return RegisterInstance(typeof(TInterface), instance, overridable, interceptors);
        }

        public IDependecyInjector RegisterInstance<TInterface>(string name, TInterface instance, bool overridable = false, params Type[] interceptors)
        {
            RegisterInstance(typeof(TInterface), name, instance, overridable);
            return this;
        }

        public IDependecyInjector RegisterInstance(Type t, object instance, bool overridable = false, params Type[] interceptors)
        {
            return RegisterInstance(t, null, instance, overridable);
        }

        public IDependecyInjector RegisterInstance(Type t, string name, object instance, bool overridable = false, params Type[] interceptors)
        {
            var componentRegistration = Component.For(t).Instance(instance).Named(name);

            if (overridable)
            {
                componentRegistration.IsFallback();
            }

            RegisterInterceptor(interceptors, componentRegistration);

            _container.Register(componentRegistration);

            return this;
        }

        public IDependecyInjector RegisterFactory(Type factoryType, InstanceMode instanceMode = InstanceMode.Transient)
        {
            ComponentRegistration<object> componentRegistration = Component.For(factoryType).AsFactory();

            SetLifestyle(componentRegistration, instanceMode);

            _container.Register(componentRegistration);

            return this;
        }

        public IDependecyInjector RegisterFactory<T>(InstanceMode instanceMode = InstanceMode.Transient)
            where T : class
        {
            Type type = typeof(T);
            RegisterFactory(type, instanceMode);

            return this;
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return _container.Resolve(name, t);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            Array resolveAll = _container.ResolveAll(t);

            return resolveAll.Cast<object>();
        }

        public T BuildUp<T>(T existing)
        {
            return _container.Kernel.Resolve<T>(new Dictionary<string, T> { { BuildUpComponentActivator.InstanceKey, existing } });
        }

        public T BuildUp<T>(T existing, string name)
        {
            return _container.Kernel.Resolve<T>(name, new Dictionary<string, T> { { BuildUpComponentActivator.InstanceKey, existing } });
        }

        public object BuildUp(Type t, object existing)
        {
            var dictionary = new Dictionary<string, object> { { BuildUpComponentActivator.InstanceKey, existing } };

            return _container.Kernel.Resolve(t, dictionary);
        }

        public object BuildUp(Type t, object existing, string name)
        {
            var dictionary = new Dictionary<string, object> { { BuildUpComponentActivator.InstanceKey, existing } };

            return _container.Kernel.Resolve(name, t, dictionary);
        }

        public void Teardown(object o)
        {
            _container.Release(o);
        }

        public bool IsRegistered(Type typeToCheck)
        {
            return _container.Kernel.HasComponent(typeToCheck);
        }

        public IDependecyInjector CreateChildContainer()
        {
            return new CastleDependencyInjector(this);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        private void RegisterInterceptor<T>(Type[] interceptors, ComponentRegistration<T> componentRegistration) where T : class
        {
            if (!interceptors.IsNullOrEmpty())
            {
                bool valid = interceptors.All(type => typeof(Castle.DynamicProxy.IInterceptor).IsAssignableFrom(type));

                if (!valid)
                {
                    string interceptorMessage = interceptors.Where(type => !typeof(Castle.DynamicProxy.IInterceptor).IsAssignableFrom(type)).Select(type => type.FullName).ToList().JoinToString(",");
                    throw new InvalidInterceptorException(string.Format("Following interceptors is Invalid: \n {0}", interceptorMessage));
                }

                componentRegistration.Interceptors(interceptors);
            }
        }

        private void SetLifestyle<T>(ComponentRegistration<T> componentRegistration, InstanceMode instanceMode) where T : class
        {
            if (instanceMode == InstanceMode.Singleton)
            {
                componentRegistration.LifestyleSingleton();
            }

            if (instanceMode == InstanceMode.Transient)
            {
                componentRegistration.LifestyleTransient();
            }

            if (instanceMode == InstanceMode.PerThread)
            {
                componentRegistration.LifestylePerThread();
            }

            if (instanceMode == InstanceMode.PerWebRequest)
            {
                componentRegistration.LifestylePerWebRequest();
            }
        }

    }
}
