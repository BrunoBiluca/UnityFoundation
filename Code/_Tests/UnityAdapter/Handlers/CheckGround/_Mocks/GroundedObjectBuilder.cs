using UnityEngine;
using UnityFoundation.Code.Math;

namespace UnityFoundation.Physics3D.CheckGround
{
    public class GroundedObjectBuilder
    {
        private GameObject groundedObject;

        public GroundedObjectBuilder()
        {
            groundedObject = new GameObject("groundedObject");
            groundedObject.transform.position = new Vector3(0, 1.5f, 0);
        }

        public GroundedObjectBuilder WithPosition(float y)
        {
            groundedObject.transform.position = groundedObject.transform.position.WithY(y);
            return this;
        }

        public GameObject Build()
        {
            var objCol = groundedObject.AddComponent<CapsuleCollider>();
            objCol.height = 2f;
            objCol.radius = 0.5f;
            return groundedObject;
        }
    }
}