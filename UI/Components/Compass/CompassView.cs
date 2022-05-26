using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Compass
{
    public class CompassView : MonoBehaviour
    {
        private RectTransform compassLine;
        private RectTransform objReference;

        public CompassController Controller { get; private set; }

        public void Setup(Transform player)
        {
            Controller = new CompassController(player);
            Controller.OnRegister += InstantiateObjectReference;
        }

        public void Awake()
        {
            compassLine = transform.FindComponent<RectTransform>("compass_line");
            objReference = compassLine.FindComponent<RectTransform>("obj_reference");
        }

        public void Update()
        {
            var corners = new Vector3[4];
            compassLine.GetLocalCorners(corners);
            var pointerScale = Vector3.Distance(corners[1], corners[2]);

            Controller.Update();

            foreach(var co in Controller.CompassObjects)
            {
                co.Value.ObjRef.localPosition = new Vector3(
                    co.Value.Angle / 180f * pointerScale,
                    co.Value.ObjRef.localPosition.y,
                    co.Value.ObjRef.localPosition.z
                );
            }
        }

        private void InstantiateObjectReference(CompassObject trackedObject)
        {
            var objRef = Instantiate(objReference, compassLine.transform);
            objRef.gameObject.SetActive(true);
            trackedObject.ObjRef = objRef;
        }
    }
}