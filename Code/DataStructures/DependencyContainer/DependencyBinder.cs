using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code
{
    public class DependencyBinder : IDependencyBinder
    {
        private readonly Dictionary<Type, IRegisteredType> types = new();

        public void Register<TInterface, TConcrete>() where TConcrete : TInterface
        {
            var interfaceType = typeof(TInterface);
            var concreteType = typeof(TConcrete);

            // base type
            IRegisteredType registeredType = new DefaultConstructorType(concreteType);

            // register types
            Register(interfaceType, registeredType);
            Register(concreteType, registeredType);
        }

        public void Register<TConcrete>()
        {
            Register(typeof(TConcrete), new DefaultConstructorType(typeof(TConcrete)));
        }

        public void Register<TConcrete>(TConcrete instance)
        {
            var concreteType = typeof(TConcrete);

            RegisterSingleton(concreteType, new ConstantType(concreteType, instance));
        }

        public void RegisterSingleton<TInterface, TConcrete>()
        {
            var interfaceType = typeof(TInterface);
            var concreteType = typeof(TConcrete);
            var registeredType = new SingletonType(new DefaultConstructorType(concreteType));

            RegisterSingleton(interfaceType, registeredType);
            RegisterSingleton(concreteType, registeredType);
        }

        public IDependencyContainer Build()
        {
            return new DependencyContainer(types);
        }

        private void CheckContainerProvider(
            Type concreteType,
            ref IRegisteredType registeredType
        )
        {
            var containerProvide = concreteType.GetInterface(nameof(IContainerProvide));
            if(containerProvide != null)
                registeredType = new ProvideContainerType(registeredType);
        }

        private void CheckDependencySetup(
            Type concreteType,
            ref IRegisteredType registeredType,
            bool setupOnlyOnce = false
        )
        {
            var dependencySetupInterfaces = concreteType
                .GetInterfaces()
                .Where(DependencySetupValidation.HasDependencySetup)
                .ToArray();

            if(dependencySetupInterfaces.Length > 0)
            {
                registeredType = new DependencySetupType(registeredType, setupOnlyOnce);
                foreach(var iface in dependencySetupInterfaces)
                    foreach(var genericArgument in iface.GetGenericArguments())
                        Register(genericArgument, new DefaultConstructorType(genericArgument));
            }
        }

        private void Register(Type concreteType, IRegisteredType registeredType)
        {
            CheckDependencySetup(concreteType, ref registeredType);
            CheckContainerProvider(concreteType, ref registeredType);

            types.TryAdd(concreteType, registeredType);
        }

        private void RegisterSingleton(Type concreteType, IRegisteredType registeredType)
        {
            CheckDependencySetup(concreteType, ref registeredType, setupOnlyOnce: true);
            CheckContainerProvider(concreteType, ref registeredType);

            types[concreteType] = registeredType;
        }

    }
}
