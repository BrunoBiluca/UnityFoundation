using UnityEngine;

namespace UnityFoundation.Code
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private bool freezeRotation;

        private Optional<Quaternion> originalRotation = Optional<Quaternion>.None();
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