using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.Zombies
{
    public class SetupWandeningHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        private readonly SimpleBrain.Settings settings;

        private Vector3 targetPosition;

        public SetupWandeningHandler(SimpleBrain.Settings settings)
        {
            this.settings = settings;
        }

        public override bool OnHandle(SimpleBrainContext context)
        {
            context.IsWandering = true;
            context.IsWalking = true;

            if(Time.time >= context.NextWanderPositionTime)
                EvaluateTargetPosition(context);

            context.TargetPosition = Optional<Vector3>.Some(targetPosition);

            var distance = Vector3.Distance(
                context.Body.position, context.TargetPosition.Get()
            );

            if(distance.NearlyEqual(0f, 0.5f))
                return false;

            return true;
        }

        private void EvaluateTargetPosition(SimpleBrainContext context)
        {
            var bodyPos = context.Body.transform.position;
            var posX = Random.Range(
                bodyPos.x - settings.WanderingDistance, 
                bodyPos.x + settings.WanderingDistance
            );
            var posZ = Random.Range(
                bodyPos.z - settings.WanderingDistance, 
                bodyPos.z + settings.WanderingDistance
            );

            var target = new Vector3(posX, 0f, posZ);

            if(Terrain.activeTerrain != null)
            {
                var posY = Terrain.activeTerrain.SampleHeight(target);
                target.y = posY;
            }

            targetPosition = target;
            context.NextWanderPositionTime = Time.time + settings.WanderingReevaluateTime;
        }
    }
}