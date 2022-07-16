using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface ITransform
    {
        Vector3 Foward { get; }
        Vector3 Position { get; set; }
        ITransform GetTransform();
    }
}
