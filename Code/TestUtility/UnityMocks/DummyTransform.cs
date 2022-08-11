using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.TestUtility
{
    public class DummyTransform : ITransform
    {
        public Vector3 Position { get; set; }
        public string Name { get; set; }
        Vector3 ITransform.Foward { get; set; }

        public ITransform GetTransform()
        {
            return this;
        }
    }
}