using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.Zombies
{
    public class DebugModeBrainHander : BaseDecisionHandler<SimpleBrainContext>
    {
        private readonly SimpleBrain.Settings settings;

        public DebugModeBrainHander(SimpleBrain.Settings settings)
        {
            this.settings = settings;
        }
        protected override bool OnDecide(SimpleBrainContext context)
        {
            if(!context.DebugMode)
                return true;

            DebugDraw.DrawSphere(
                context.Body.position, settings.MinAttackDistance, Color.red);
            DebugDraw.DrawSphere(
                context.Body.position, settings.MinDistanceForChasePlayer, Color.blue);

            return true;
        }
    }
}