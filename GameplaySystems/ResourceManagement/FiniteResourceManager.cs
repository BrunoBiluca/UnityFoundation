using System;
using UnityFoundation.Code;

namespace UnityFoundation.ResourceManagement
{
    public class FiniteResourceManager : IResourceManager
    {
        private uint currentAmount;
        public uint CurrentAmount => currentAmount;

        private readonly uint maxAmount;
        public uint MaxAmount => maxAmount;

        public bool IsEmpty => currentAmount == 0;

        public bool IsFull => maxAmount == currentAmount;

        public event Action<IResourceManager> OnResourceChanged;

        public FiniteResourceManager(uint maxAmount, bool startFull = false)
        {
            this.maxAmount = maxAmount;

            if(startFull)
                currentAmount = maxAmount;
        }

        public void Add(uint amount)
        {
            currentAmount += amount;
            currentAmount = currentAmount.Clamp(0, MaxAmount);
        }

        public void FullReffil()
        {
            currentAmount = maxAmount;
        }

        public uint GetAmount(uint amount)
        {
            if(currentAmount < amount)
            {
                var auxResult = currentAmount;
                currentAmount = 0;
                return auxResult;
            }

            Subtract(amount);
            return amount;
        }

        public bool TrySubtract(uint amount)
        {
            if(amount > currentAmount)
                return false;

            Subtract(amount);
            return true;
        }

        private void Subtract(uint amount)
        {
            currentAmount -= amount;
            OnResourceChanged?.Invoke(this);
        }

        public void Emptify()
        {
            currentAmount = 0;
        }
    }
}