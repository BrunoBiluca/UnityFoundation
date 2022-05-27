using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;
using UnityFoundation.Code.Guard;

namespace UnityFoundation.Radar
{
    public class RadarView : MonoBehaviour
    {
        [field: SerializeField] public float MapScale { get; set; } = 1f;

        public Image PlayerRef { get; private set; }

        private RectTransform objectRef;
        private RectTransform panelRef;

        public int ActiveTrackObjects => trackedObjects.Count;

        private Transform playerTransform;
        private Dictionary<Transform, RadarObject> trackedObjects;

        public void Setup(Transform player)
        {
            playerTransform = Guard.Required(player, "Player");
            PlayerRef.gameObject.SetActive(true);
        }

        public void Register(Transform trackedObject)
        {
            var objRef = Instantiate(objectRef, panelRef);
            objRef.gameObject.SetActive(true);
            trackedObjects.Add(trackedObject, new RadarObject(trackedObject, objRef));
        }

        public void Register(Transform trackedObject, GameObject objRefPrefab)
        {
            if(objRefPrefab == null)
            {
                Register(trackedObject);
                return;
            }

            var objRef = Instantiate(objRefPrefab, panelRef);
            objRef.SetActive(true);

            if(!objRef.TryGetComponent(out RectTransform rectTransform))
                throw new MissingComponentException("Missing RectTransform on objRefPrefab");

            trackedObjects.Add(
                trackedObject,
                new RadarObject(trackedObject, rectTransform)
            );
        }

        public void UnRegister(Transform untrackedObject)
        {
            if(!trackedObjects.ContainsKey(untrackedObject))
                return;

            if(
                untrackedObject != null
                && trackedObjects[untrackedObject].ObjectRef != null
            )
                Destroy(trackedObjects[untrackedObject].ObjectRef.gameObject);

            trackedObjects.Remove(untrackedObject);
        }

        public void Awake()
        {
            trackedObjects = new Dictionary<Transform, RadarObject>();
            panelRef = transform.FindComponent<RectTransform>("radar_holder", "panel");
            PlayerRef = panelRef.FindComponent<Image>("player_ref");
            objectRef = panelRef.FindComponent<RectTransform>("object_ref");
        }

        public void Update()
        {
            if(playerTransform == null) return;

            foreach(var ro in trackedObjects)
            {
                var radarPos = ro.Value.TransformRef.position - playerTransform.position;
                var distToObject = Vector3.Distance(
                    playerTransform.position, ro.Value.TransformRef.position
                ) * MapScale;

                var deltay = Mathf.Atan2(radarPos.x, radarPos.z)
                    * Mathf.Rad2Deg
                    - 270
                    - playerTransform.eulerAngles.y;

                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

                ro.Value.ObjectRef.transform.position = new Vector3(
                    radarPos.x + panelRef.pivot.x,
                    radarPos.z + panelRef.pivot.y,
                    0
                ) + panelRef.position;
            }
        }
    }
}