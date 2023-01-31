using System;
using System.Collections;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UnityFoundation.Code
{
    public sealed class DependencyBinder : IDependencyBinder
    {
        private readonly RegistryTypes registeredTypes = new();

        public void Register<TInterface, TConcrete>() where TConcrete : TInterface
        {
            var typeBuilder = RegistryTypeBuilder
                .WithDefaultConstructor(typeof(TConcrete))
                .AsInterface(typeof(TInterface));
            Register(ref typeBuilder);
        }

        public void Register<TConcrete>()
        {
            Register<TConcrete, TConcrete>();
        }

        public void Register<TConcrete>(TConcrete instance)
        {
            var typeBuilder = RegistryTypeBuilder.WithConstant(typeof(TConcrete), instance);
            Register(ref typeBuilder);
        }

        public void Register<TInterface, TConcrete>(Enum key) where TConcrete : TInterface
        {
            var typeBuilder = RegistryTypeBuilder
                .WithDefaultConstructor(typeof(TConcrete))
                .AsInterface(typeof(TInterface))
                .WithKey(key);
            Register(ref typeBuilder);
        }

        public void RegisterSingleton<TInterface, TConcrete>()
        {
            var typeBuilder = RegistryTypeBuilder
                .WithDefaultConstructor(typeof(TConcrete))
                .AsSingleton()
                .AsInterface(typeof(TInterface));

            Register(ref typeBuilder);
        }

        public IDependencyContainer Build()
        {
            return new DependencyContainer(registeredTypes);
        }

        private void Register(ref RegistryTypeBuilder typeBuilder)
        {
            CheckDependencySetup(ref typeBuilder);
            CheckContainerProvider(ref typeBuilder);

            registeredTypes.Add(typeBuilder);
        }

        private void CheckContainerProvider(ref RegistryTypeBuilder typeBuilder)
        {
            var containerProvide = typeBuilder.ConcreteType.GetInterface(nameof(IContainerProvide));
            if(containerProvide != null)
                typeBuilder.AddProvideContainer();
        }

        private void CheckDependencySetup(ref RegistryTypeBuilder typeBuilder)
        {
            var dependencySetupInterfaces = typeBuilder.ConcreteType
                .GetInterfaces()
                .Where(DependencySetupValidation.HasDependencySetup)
                .ToArray();

            if(dependencySetupInterfaces.Length > 0)
            {
                typeBuilder.AddDependencySetup();
                foreach(var iface in dependencySetupInterfaces)
                    foreach(var genericArgument in iface.GetGenericArguments())
                    {
                        var argumentTypeBuilder = RegistryTypeBuilder
                            .WithDefaultConstructor(genericArgument);
                        Register(ref argumentTypeBuilder);
                    }
            }
        }
    }
}
