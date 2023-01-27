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

        public DependencyContainer(Dictionary<Type, IRegisteredType> registeredTypes)
        {
            types = registeredTypes;
        }

        public void RegisterAction<TInterface>(Action<TInterface> creationAction)
        {
            registeredActions[typeof(TInterface)] = (obj) => creationAction((TInterface)obj);
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        public object Resolve(Type type)
        {
            if(!types.ContainsKey(type))
                throw new TypeNotRegisteredException(type);

            var registeredType = types[type];
            var instance = registeredType.Instantiate(this);

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

        public void Setup<T>(IDependencySetup<T> instance)
        {
            instance.Setup(Resolve<T>());
        }

        public void Setup<T1, T2>(IDependencySetup<T1, T2> instance)
        {
            instance.Setup(Resolve<T1>(), Resolve<T2>());
        }

        public void Setup<T1, T2, T3>(IDependencySetup<T1, T2, T3> instance)
        {
            instance.Setup(Resolve<T1>(), Resolve<T2>(), Resolve<T3>());
        }

        public void Setup<T1, T2, T3, T4>(IDependencySetup<T1, T2, T3, T4> instance)
        {
            instance.Setup(Resolve<T1>(), Resolve<T2>(), Resolve<T3>(), Resolve<T4>());
        }
    }
}
