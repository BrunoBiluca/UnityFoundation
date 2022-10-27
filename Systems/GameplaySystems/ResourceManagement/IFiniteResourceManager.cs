namespace UnityFoundation.ResourceManagement
{
    public interface IResourceManager
    {
        uint CurrentAmount { get; }

        uint MaxAmount { get; }
        bool IsFull { get; }

        void FullReffil();
        uint GetAmount(uint amount);
        void Add(uint amount);
    }
}