using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public interface ITransform
    {
        string Name { get; set; }
        Vector3 Foward { get; set; }
        Vector3 Position { get; set; }
        ITransform GetTransform();
    }
}
