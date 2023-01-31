﻿using System;

namespace UnityFoundation.Code
{
    public sealed class RegistryTypeBuilder
    {
        public static RegistryTypeBuilder WithDefaultConstructor(Type concreteType)
        {
            return new RegistryTypeBuilder(new DefaultConstructorType(concreteType));
        }

        public static RegistryTypeBuilder WithConstant(Type concreteType, object instance)
        {
            return new RegistryTypeBuilder(new ConstantType(concreteType, instance)) {
                ForceRegister = true,
                SetupOnce = true
            };
        }

        private RegistryTypeBuilder(IRegisteredType startType)
        {
            current = startType;
        }

        private IRegisteredType current;

        public Type ConcreteType => current.ConcreteType;
        public Type InterfaceType { get; private set; } = null;
        public bool ForceRegister { get; private set; } = false;
        public bool SetupOnce { get; private set; } = false;
        public Enum Key { get; private set; }

        public RegistryTypeBuilder AsInterface(Type type)
        {
            InterfaceType = type;
            return this;
        }

        public RegistryTypeBuilder AsSingleton()
        {
            current = new SingletonType(current);
            ForceRegister = true;
            return this;
        }

        public RegistryTypeBuilder AddProvideContainer()
        {
            current = new ProvideContainerType(current);
            return this;
        }

        public RegistryTypeBuilder AddDependencySetup()
        {
            current = new DependencySetupType(current, SetupOnce);
            return this;
        }

        public RegistryTypeBuilder WithKey(Enum key)
        {
            Key = key;
            return this;
        }

        public IRegisteredType Build()
        {
            return current;
        }
    }
}
