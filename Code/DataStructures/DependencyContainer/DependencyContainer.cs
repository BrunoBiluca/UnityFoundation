using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityFoundation.Code
{
    public sealed class DependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, Action<object>> registeredActions = new();
        private readonly RegistryTypes registry;
        private List<object> parameters;

        public DependencyContainer(RegistryTypes registry)
        {
            this.registry = registry;
        }

        public void RegisterAction<TInterface>(Action<TInterface> creationAction)
        {
            registeredActions[typeof(TInterface)] = (obj) => creationAction((TInterface)obj);
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        public TInterface Resolve<TInterface>(params object[] parameters)
        {
            var type = typeof(TInterface);
            for(int i = 0; i < parameters.Length; i++)
            {
                if(parameters[i] == null)
                    throw new ArgumentNullException(
                        $"When resolving {type} found a null parameter on <{i}> position");
            }

            this.parameters = parameters.ToList();
            var instance = (TInterface)Resolve(typeof(TInterface));
            this.parameters = null;
            return instance;
        }

        public TInterface Resolve<TInterface>(Enum key)
        {
            return (TInterface)Resolve(typeof(TInterface), key);
        }

        public object Resolve(Type type)
        {
            if(TryToResolveParameter(type, out object obj))
            {
                parameters.Remove(obj);
                return obj;
            }

            return Instantiate(registry.GetRegistered(type));
        }

        public object Resolve(Type type, Enum key)
        {
            return Instantiate(registry.GetRegistered(type, key));
        }

        private bool TryToResolveParameter(Type resolveType, out object obj)
        {
            obj = null;
            if(parameters == null)
                return false;

            foreach(var param in parameters)
            {
                if(!resolveType.IsAssignableFrom(param.GetType()))
                    continue;

                obj = param;
                return true;
            }

            return false;
        }

        private object Instantiate(IRegisteredType type)
        {
            var instance = type.Instantiate(this);
            // TODO: PostCreationActions pode ser setado no momento do registro
            PostCreationActions(type.ConcreteType, ref instance);
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
