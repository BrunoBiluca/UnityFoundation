using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface ICollider
    {
        ITransform Transform { get; }
        Bounds Bounds { get; }
        float Height { get; set; }
    }
}
