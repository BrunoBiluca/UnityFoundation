using System;

namespace UnityFoundation.Code
{
    public interface IRegisteredType
    {
        Type ConcreteType { get; }

        public object Instantiate(IDependencyContainer container);
    }
}
