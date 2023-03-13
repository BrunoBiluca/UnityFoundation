using UnityEngine;

namespace UnityFoundation.Physics3D.CheckGround
{
    public class FloorBuilder
    {
        public GameObject Build()
        {
            var floor = new GameObject("floor");
            floor.transform.position = new Vector3(0, 0, 0);
            var floorCol = floor.AddComponent<BoxCollider>();
            floorCol.size = new Vector3(1, 1, 1);
            return floor;
        }
    }
}