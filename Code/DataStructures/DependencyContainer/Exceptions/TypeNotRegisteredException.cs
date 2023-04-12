using System;

namespace UnityFoundation.Code
{
    public sealed class TypeNotRegisteredException : Exception
    {
        const string msg = "<type> was not registered in the context container";
        public TypeNotRegisteredException(Type type)
            : base(msg.Replace("<type>", type.ToString()))
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
