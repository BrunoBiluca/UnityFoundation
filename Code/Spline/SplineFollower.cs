using Assets.UnityFoundation.Code;
using UnityEngine;

namespace Assets.UnityFoundation.Code.Spline
{
    public class SplineFollower : MonoBehaviour
    {
        [SerializeField] private SplineMonoBehaviour spline;

        [SerializeField] private float followDuration = 1f;
        [SerializeField] private bool followLoop;

        private float interpolateAmount;

        void Update()
        {
            interpolateAmount += Time.deltaTime;

            if(followLoop)
                interpolateAmount %= followDuration;

            if(interpolateAmount > followDuration)
                return;

            transform.position = spline.GetPosition(
                interpolateAmount.Remap(0f, followDuration, 0f, 1f)
            );
        }
    }
}