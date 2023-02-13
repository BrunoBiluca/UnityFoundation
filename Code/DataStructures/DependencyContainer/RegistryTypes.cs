using System;
using System.Collections.Generic;

namespace UnityFoundation.Code
{
    public class RegistryKey
    {
        public Enum Key { get; }
        public Type Type { get; }

        public RegistryKey(Type type)
        {
            Type = type;
        }

        public RegistryKey(Type type, Enum key) : this(type)
        {
            Key = key;
        }

        public override string ToString()
        {
            var str = Type.ToString();
            if(Key != null) str += "_" + Key.ToString();
            return str;
        }

        public override int GetHashCode()
        {
            var keyHash = Key != null ? Key.GetHashCode() : 0;
            return Type.GetHashCode() ^ keyHash;
        }

        public override bool Equals(object obj)
        {
            if(obj is not RegistryKey y)
                return false;

            return Type == y.Type && Equals(Key, y.Key);
        }
    }

    public sealed class RegistryTypes
    {
        private readonly Dictionary<RegistryKey, IRegisteredType> types = new();

        public void Add(RegistryTypeBuilder builder)
        {
            Action<RegistryKey, IRegisteredType> registerFunc = builder.ForceRegister
                ? ForceAdd
                : TryAdd;

            var type = builder.Build();
            if(builder.InterfaceType != null)
                registerFunc(GetKey(builder.InterfaceType, builder.Key), type);

            registerFunc(GetKey(builder.ConcreteType, builder.Key), type);
        }

        private void TryAdd(RegistryKey key, IRegisteredType registered)
        {
            types.TryAdd(key, registered);
        }

        private void ForceAdd(RegistryKey key, IRegisteredType registered)
        {
            types[key] = registered;
        }

        public IRegisteredType GetRegistered(Type type, Enum key = null)
        {
            var registryKey = GetKey(type, key);
            if(!types.ContainsKey(registryKey))
                throw new TypeNotRegisteredException(type);

            return types[registryKey];
        }

        private RegistryKey GetKey(Type type, Enum key = null)
        {
            return new RegistryKey(type, key);
        }
    }
}
