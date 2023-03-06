using System;

namespace UnityFoundation.ResourceManagement
{
    // TODO: definir a diferenção entre os métodos GetAmount e TrySubstract
    // ambos métodos fazem a mesma coisa de formas diferentes.

    public interface IResourceManager
    {
        uint CurrentAmount { get; }

        uint MaxAmount { get; }
        bool IsFull { get; }
        bool IsEmpty { get; }

        event Action<IResourceManager> OnResourceChanged;

        void FullReffil();
        void Emptify();

        uint GetAmount(uint amount);
        void Add(uint amount);
        bool TrySubtract(uint amount);
    }
}