using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.DiceSystem
{
    public interface IDiceMono : IDice
    {
        ITransform GetTransform();
        IRigidbody GetRigidbody();
        IDiceSide CheckSelectedSide();
    }
}