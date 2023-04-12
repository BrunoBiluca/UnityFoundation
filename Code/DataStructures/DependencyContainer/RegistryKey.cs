using System;

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
}
