using UnityEngine;

namespace UnityFoundation.Radar
{
    public class RadarTrackedObject : MonoBehaviour
    {
        [field: SerializeField] public RadarView RadarView { get; protected set; }

        [field: SerializeField] private GameObject objRefPrefab;

        public void Start()
        {
            RadarView.Register(transform, objRefPrefab);
        }

        public void UnRegister()
        {
            RadarView.UnRegister(transform);
        }

        public void OnDestroy()
        {
            RadarView.UnRegister(transform);
        }
    }
}