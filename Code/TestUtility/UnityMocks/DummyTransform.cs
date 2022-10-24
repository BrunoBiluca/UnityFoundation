using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.TestUtility
{
    public class DummyTransform : ITransform
    {
        public Vector3 Position { get; set; }
        public string Name { get; set; }
        public Quaternion Rotation { get; set; }
        Vector3 ITransform.Foward { get; set; }

        public ITransform GetTransform()
        {
            return this;
        }

        public void Rotate(Vector3 eulers)
        {
            throw new System.NotImplementedException();
        }
    }
}