using UnityEngine;

namespace UnityFoundation.Compass
{
    public class CompassObject
    {

        public Transform Obj { get; private set; }
        public float Angle { get; set; }
        public float Distance { get; set; }
        public RectTransform ObjRef { get; set; }

        public CompassObject(Transform obj)
        {
            Obj = obj;
        }
    }
}