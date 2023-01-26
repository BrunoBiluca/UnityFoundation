using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code
{

    public sealed class DependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, IRegisteredType> types = new();

        // utilizados para chamar uma função de callback quando o objeto é criado
        private readonly Dictionary<Type, Action<object>> registeredActions = new();

        public void Register<TInterface, TConcrete>() where TConcrete : TInterface
        {
            var interfaceType = typeof(TInterface);
            var concreteType = typeof(TConcrete);

            // base type
            IRegisteredType registeredType = new DefaulConstructorType(this, concreteType);

            // check dependency setup type
            var dependencySetupInterfaces = concreteType
                .GetInterfaces()
                .Where(DependencySetupType.HasDependencySetup)
                .ToArray();

            if(dependencySetupInterfaces.Length > 0)
            {
                registeredType = new DependencySetupType(this, registeredType);
                foreach(var iface in dependencySetupInterfaces)
                    foreach(var genericArgument in iface.GetGenericArguments())
                        Register(genericArgument, new DefaulConstructorType(this, genericArgument));
            }

            // check container provide
            var containerProvide = concreteType.GetInterface(nameof(IContainerProvide));
            if(containerProvide != null)
                registeredType = new ProvideContainerType(this, registeredType);


            // register types
            Register(interfaceType, registeredType);
            Register(concreteType, registeredType);
        }

        public void Register<TConcrete>()
        {
            Register<TConcrete, TConcrete>();
        }

        public void Register<TConcrete>(TConcrete instance)
        {
            var concreteType = typeof(TConcrete);
            Register(concreteType, new ConstantInstanceType(concreteType, instance));
        }

        public void RegisterSingleton<TInterface, TConcrete>()
        {
            var interfaceType = typeof(TInterface);
            var concreteType = typeof(TConcrete);
            Register(
                interfaceType,
                new SingletonInstanceType(new DefaulConstructorType(this, concreteType))
            );
        }
        private void Register(Type key, IRegisteredType registeredType)
        {
            types.TryAdd(key, registeredType);
        }

        public void RegisterAction<TInterface>(Action<TInterface> creationAction)
        {
            registeredActions[typeof(TInterface)] = (obj) => creationAction((TInterface)obj);
        }

        public void Setup<T>(IDependencySetup<T> instance)
        {
            Register<T>();
            instance.Setup(Create<T>());
        }

        public void Setup<T1, T2>(IDependencySetup<T1, T2> instance)
        {
            Register<T1>();
            Register<T2>();
            instance.Setup(Create<T1>(), Create<T2>());
        }

        public TInterface Create<TInterface>()
        {
            return (TInterface)Create(typeof(TInterface));
        }

        public object Create(Type type)
        {
            if(!types.ContainsKey(type))
                throw new TypeNotRegisteredException(type);

            var registeredType = types[type];
            var instance = registeredType.Instantiate();

            // TODO: PostCreationActions pode ser setado no momento do registro
            PostCreationActions(type, ref instance);

            return instance;
        }

        private void PostCreationActions(Type type, ref object instance)
        {
            foreach(var createdAction in registeredActions)
            {
                if(!createdAction.Key.IsAssignableFrom(type))
                    continue;

                createdAction.Value(instance);
            }
        }
    }
}
