using System;
using UnityEngine;

namespace UnityFoundation.ResourceManagement
{
    public class FiniteResourceManagerMono : MonoBehaviour, IResourceManager
    {
        private IResourceManager storage;

        public event Action<IResourceManager> OnResourceChanged;

        public FiniteResourceManagerMono Setup(IResourceManager storage)
        {
            this.storage = storage;
            return this;
        }

        public uint CurrentAmount => storage.CurrentAmount;

        public uint MaxAmount => storage.MaxAmount;

        public bool IsFull => storage.IsFull;

        public bool IsEmpty => storage.IsEmpty;

        public void Add(uint amount) => storage.Add(amount);

        public void FullReffil() => storage.FullReffil();

        public uint GetAmount(uint amount) => storage.GetAmount(amount);

        public bool TrySubtract(uint amount) => storage.TrySubtract(amount);

        public void Emptify() => storage.Emptify();
    }
}