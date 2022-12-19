using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Tools.Spline
{
    public class SplineFollower : MonoBehaviour
    {
        [SerializeField] private SplineMono spline;

        [SerializeField] private float followDuration = 1f;

        [StringInList(
            typeof(EnumX<SplineFollowBehaviour>), nameof(SplineFollowBehaviour.Values)
        )]
        public string followBehaviour;

        private float interpolateAmount;

        private float backAndForthDirection = 1;

        public SplineFollower Setup(
            SplineMono spline,
            SplineFollowBehaviour followBehaviour
        )
        {
            this.spline = spline;
            this.followBehaviour = followBehaviour;
            interpolateAmount = 0f;
            backAndForthDirection = 1f;

            enabled = true;
            return this;
        }

        void Update()
        {
            if(spline == null)
            {
                enabled = false;
                return;
            }


            if(followBehaviour == SplineFollowBehaviour.oneGo)
                OneGoEvaluateInterpolateAmount();
            else if(followBehaviour == SplineFollowBehaviour.loop)
                LoopEvaluateInterpolateAmount();
            else if(followBehaviour == SplineFollowBehaviour.backAndForth)
                BackAndForthEvaluateInterpolateAmount();

            transform.position = spline.GetPosition(
                interpolateAmount.Remap(0f, followDuration, 0f, 1f)
            );
        }

        private void OneGoEvaluateInterpolateAmount()
        {
            interpolateAmount += Time.deltaTime;
        }

        private void LoopEvaluateInterpolateAmount()
        {
            interpolateAmount += Time.deltaTime;
            interpolateAmount %= followDuration;
        }

        private void BackAndForthEvaluateInterpolateAmount()
        {
            interpolateAmount += Time.deltaTime * backAndForthDirection;

            if(interpolateAmount >= followDuration)
            {
                backAndForthDirection = -1;
                interpolateAmount = followDuration;
            }

            if(interpolateAmount <= 0f)
            {
                backAndForthDirection = 1;
                interpolateAmount = 0f;
            }

        }
    }
}