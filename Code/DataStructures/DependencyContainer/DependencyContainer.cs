using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code
{

    public sealed class DependencyContainer : IDependencyContainer
    {
        private readonly Dictionary<Type, Type> registeredTypes = new();
        private readonly Dictionary<Type, object> registeredObjects = new();
        private readonly Dictionary<Type, Action<object>> registeredActions = new();

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            var interfaceType = typeof(TInterface);
            var implType = typeof(TImplementation);

            Register(interfaceType, implType);
            Register(implType, implType);
            RegisterIfHasDependencySetup(implType);
        }

        private void RegisterIfHasDependencySetup(Type implType)
        {
            var interfaces = implType.GetInterfaces();

            foreach(var iface in interfaces.Where(HasDependencySetup))
                foreach(var genericArgument in iface.GetGenericArguments())
                    Register(genericArgument, genericArgument);
        }

        private bool HasDependencySetup(Type t)
        {
            return t.IsGenericType
              && (
                  t.GetGenericTypeDefinition() == typeof(IDependencySetup<>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,,>)
                  || t.GetGenericTypeDefinition() == typeof(IDependencySetup<,,,>)
              );
        }

        public void Register<TImplementation>()
        {
            Register<TImplementation, TImplementation>();
        }

        public void Register<TInterface>(Action<TInterface> creationAction)
        {
            registeredActions[typeof(TInterface)] = (obj) => creationAction((TInterface)obj);
        }

        public void Register<TInterface>(TInterface instance)
        {
            registeredObjects[typeof(TInterface)] = instance;
        }

        public TInterface Create<TInterface>()
        {
            return (TInterface)Create(typeof(TInterface));
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

        public void Register(Type key, Type impl)
        {
            if(registeredObjects.ContainsKey(key))
                return;
            registeredTypes[key] = impl;
        }

        private object Create(Type type)
        {
            if(registeredObjects.ContainsKey(type))
                return registeredObjects[type];

            if(!registeredTypes.ContainsKey(type))
                throw new TypeNotRegisteredException(type);

            var concreteType = registeredTypes[type];
            var defaultConstructor = concreteType.GetConstructors()[0];

            //Verify if the default constructor requires params
            var defaultParams = defaultConstructor.GetParameters();

            //Instantiate all constructor parameters using recursion
            var parameters = defaultParams
                .Select(param => Create(param.ParameterType))
                .ToArray();

            var instance = defaultConstructor.Invoke(parameters);

            PostCreationActions(type, ref instance);
            InjectDependencySetup(concreteType, ref instance);

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

        private void InjectDependencySetup(Type concreteType, ref object instance)
        {
            var hasDependendySetup = concreteType
                .GetInterfaces()
                .Any(HasDependencySetup);

            if(hasDependendySetup)
            {
                // TODO: iterar apenas pelos métodos que implementam a interaface IDependencySetup
                var allSetupMethods = concreteType
                    .GetMethods()
                    .Where(m => m.Name.Contains("Setup"));
                foreach(var method in allSetupMethods)
                {
                    var methodParameters = method.GetParameters()
                        .Select(param => Create(param.ParameterType))
                        .ToArray();
                    method.Invoke(instance, methodParameters);
                }
            }
        }
    }
}
