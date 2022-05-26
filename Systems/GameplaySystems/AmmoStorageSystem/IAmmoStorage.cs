namespace UnityFoundation.AmmoStorageSystem
{
    public interface IAmmoStorage
    {
        uint CurrentAmount { get; }

        uint MaxAmount { get; }
        bool IsFull { get; }

        void FullReffil();
        uint GetAmmo(uint amount);
        void Recover(uint amount);
    }
}