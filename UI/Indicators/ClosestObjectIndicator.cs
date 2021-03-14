using Assets.UnityFoundation.TransformUtils;
using Assets.UnityFoundation.UI.Indicators;
using UnityEngine;

namespace Assets.UnityFoundation.UI.Indicators {
    public class ClosestObjectIndicator : MonoBehaviour {

        [SerializeField]
        private GameObject indicablePrefab;

        [SerializeField]
        private Transform referenceTransform;

        RectTransform rectTransform;
        ClosestTransformCircleFinder transformFinder;

        private GameObject image;

        void Awake() {
            transformFinder = gameObject.AddComponent<ClosestTransformCircleFinder>();
            transformFinder.Setup(
                indicablePrefab.GetComponent<IIndicable>().GetType(),
                referenceTransform: referenceTransform,
                lookRangeRadius: 40f
            );

            rectTransform = GetComponent<RectTransform>();

            image = transform.Find("image").gameObject;
        }

        private void Update() {
            transformFinder.Target
                .Some(target => UpdateIndicator(target))
                .OrElse(() => image.SetActive(false));
        }

        private void UpdateIndicator(Transform target) {
            Camera camera = Camera.main;

            var distanceToClosest = Vector3.Distance(target.position, camera.transform.position);
            if(distanceToClosest < camera.orthographicSize * 3f) {
                image.SetActive(false);
                return;
            }

            image.SetActive(true);
            var distance = (target.position - camera.transform.position).normalized;
            rectTransform.anchoredPosition = distance * 250f;
            rectTransform.eulerAngles = RotationUtils.GetZRotation(distance);
        }
    }
}