using UnityFoundation.Code;

namespace UnityFoundation.AmmoStorageSystem
{
    public class AmmoStorage : IAmmoStorage
    {
        private uint currentAmount;
        public uint CurrentAmount => currentAmount;

        private readonly uint maxAmount;
        public uint MaxAmount => maxAmount;

        public bool Empty => currentAmount == 0;

        public bool IsFull => maxAmount == currentAmount;

        public AmmoStorage(uint maxAmount, bool startFull = false)
        {
            this.maxAmount = maxAmount;

            if(startFull)
                currentAmount = maxAmount;
        }

        public void Recover(uint amount)
        {
            currentAmount += amount;
            currentAmount = currentAmount.Clamp(0, MaxAmount);
        }

        public void FullReffil()
        {
            currentAmount = maxAmount;
        }

        public uint GetAmmo(uint amount)
        {
            if(currentAmount < amount)
            {
                var auxResult = currentAmount;
                currentAmount = 0;
                return auxResult;
            }

            currentAmount -= amount;
            return amount;
        }
    }
}