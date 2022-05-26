using UnityEngine;
using UnityEngine.UI;

namespace UnityFoundation.Radar
{
    public class RadarObject
    {
        public Transform TransformRef { get; private set; }
        public RectTransform ObjectRef { get; private set; }

        public RadarObject(Transform transformRef, RectTransform objectRef)
        {
            TransformRef = transformRef;
            ObjectRef = objectRef;
        }
    }
}