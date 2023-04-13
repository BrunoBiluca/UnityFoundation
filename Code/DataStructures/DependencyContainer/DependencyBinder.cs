using System;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code
{
    public sealed class DependencyBinder : IDependencyBinder
    {
        private readonly RegistryTypes registeredTypes = new();

        public void Register<TInterface, TConcrete>(Enum key = null) where TConcrete : TInterface
        {
            var typeBuilder = RegistryTypeBuilder
                .WithDefaultConstructor(typeof(TConcrete))
                .AsInterface(typeof(TInterface))
                .WithKey(key);
            Register(ref typeBuilder);
        }

        public void Register<TConcrete>(Enum key = null)
        {
            var registerType = typeof(TConcrete);
            if(registerType.IsInterface)
                throw new ArgumentException("Can't register interfaces only");
            Register<TConcrete, TConcrete>();
        }

        public void Register<TConcrete>(TConcrete instance, Enum key = null)
        {
            var registerType = typeof(TConcrete);
            if(instance == null)
                throw new ConstantNullException(registerType);

            var typeBuilder = RegistryTypeBuilder
                .WithConstant(instance.GetType(), instance)
                .WithKey(key);

            if(registerType.IsInterface)
                typeBuilder.AsInterface(registerType);

            Register(ref typeBuilder);
        }

        public void RegisterFactory<TFactory, TInterface>(bool isSingleton = false)
        {
            Register<TFactory>();

            var typeBuilder = RegistryTypeBuilder
                .WithFactoryConstructor(typeof(TFactory), typeof(TInterface), isSingleton);

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

        public void RegisterModule(IDependencyModule module)
        {
            module.Register(this);
        }

        public void RegisterSetup<TConcrete>(TConcrete instance)
        {
            var typeBuilder = RegistryTypeBuilder
                .WithConstant(typeof(TConcrete), instance)
                .AddDependencySetupByReflection();

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
                        if(genericArgument.IsClass)
                        {
                            var argumentTypeBuilder = RegistryTypeBuilder
                                .WithDefaultConstructor(genericArgument);
                            Register(ref argumentTypeBuilder);
                        }
                    }
            }
        }
    }
}
