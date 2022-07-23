using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.TestUtility
{
    public class DummyTransform : ITransform
    {
        public Vector3 Foward => throw new System.NotImplementedException();

        public Vector3 Position { get; set; }

        public ITransform GetTransform()
        {
            return this;
        }
    }
}