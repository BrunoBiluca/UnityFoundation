using UnityEngine;

namespace UnityFoundation.Compass
{
    public class CompassTrackedObject : MonoBehaviour
    {
        [field: SerializeField] public CompassView CompassView { get; protected set; }

        public void Start()
        {
            if(CompassView != null)
                CompassView.Controller.Register(transform);
        }

        public void Setup(CompassView compass)
        {
            CompassView = compass;
            CompassView.Controller.Register(transform);
        }
    }
}