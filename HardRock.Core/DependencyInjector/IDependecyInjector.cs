using System;
using System.Collections.Generic;

namespace HardRock.Core.DependencyInjector
{
    public interface IDependecyInjector
    {
        IDependecyInjector RegisterType<TService>(InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
          where TService : class;

        IDependecyInjector RegisterType<TInterface, TService>(InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
            where TService : TInterface
            where TInterface : class;

        IDependecyInjector RegisterType<TInterface, TTo>(string name, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors)
            where TTo : TInterface
            where TInterface : class;

        IDependecyInjector RegisterType(Type @interface, Type service, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors);

        IDependecyInjector RegisterType(Type @interface, Type service, string name = null, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors);

        IDependecyInjector RegisterInstance<TInterface>(TInterface instance, bool overridable = false, params Type[] interceptors);

        IDependecyInjector RegisterInstance<TInterface>(string name, TInterface instance, bool overridable = false, params Type[] interceptors);

        IDependecyInjector RegisterInstance(Type t, object instance, bool overridable = false, params Type[] interceptors);

        IDependecyInjector RegisterInstance(Type t, string name, object instance, bool overridable = false, params Type[] interceptors);

        T Resolve<T>();

        T Resolve<T>(string name);

        object Resolve(Type t);

        object Resolve(Type t, string name);

        IEnumerable<T> ResolveAll<T>();

        IEnumerable<object> ResolveAll(Type t);

        T BuildUp<T>(T existing);

        T BuildUp<T>(T existing, string name);

        object BuildUp(Type t, object existing);

        object BuildUp(Type t, object existing, string name);

        void Teardown(object o);

        bool IsRegistered(Type typeToCheck);

        IDependecyInjector CreateChildContainer();

        IDependecyInjector RegisterMultiple(
            Type service,
            List<Type> interfaces,
            string name = null,
            InstanceMode instanceMode = InstanceMode.Transient,
            bool overridable = false,
            params Type[] interceptors);

        IDependecyInjector Configure();

        IDependecyInjector RegisterType(Type serviceType, InstanceMode instanceMode = InstanceMode.Transient, bool overridable = false, params Type[] interceptors);

        IDependecyInjector RegisterFactory(Type factoryType, InstanceMode instanceMode = InstanceMode.Transient);

        IDependecyInjector RegisterFactory<T>(InstanceMode instanceMode = InstanceMode.Transient)
            where T : class;
    }
}