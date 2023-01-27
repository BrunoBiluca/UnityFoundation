using System;

namespace UnityFoundation.Code
{
    public sealed class TypeWithNoDefaultConstructorException : Exception
    {
        const string msg = "<type> has no default constructor";

        public TypeWithNoDefaultConstructorException(Type type)
            : base(msg.Replace("<type>", type.ToString()))
        {
        }
    }
}
