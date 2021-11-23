using Assets.UnityFoundation.Code.Common;
using UnityEngine;

namespace Assets.UnityFoundation.Code
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private bool freezeRotation;

        private Optional<Quaternion> originalRotation = Optional<Quaternion>.None();

        [SerializeField] private Transform center;
        private Vector3 originalPosition;

        void Awake()
        {
            originalPosition = transform.position;

            if(freezeRotation)
                originalRotation = Optional<Quaternion>.Some(transform.rotation);
        }

        void LateUpdate()
        {
            originalRotation.Some(rotation => transform.rotation = rotation);

            transform.position = new Vector3(
                center.position.x,
                originalPosition.y,
                center.position.z
            );
        }
    }
}