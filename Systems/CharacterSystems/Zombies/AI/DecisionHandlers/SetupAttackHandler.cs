using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.Zombies
{
    public class SetupAttackHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        private readonly SimpleBrain.Settings settings;
        private readonly Transform player;

        public SetupAttackHandler(SimpleBrain.Settings settings, Transform player)
        {
            this.settings = settings;
            this.player = player;
        }

        protected override bool OnDecide(SimpleBrainContext context)
        {
            context.NextAttackTime = Time.time + settings.MinNextAttackDelay;
            context.IsAttacking = true;
            context.Target = Optional<Transform>.Some(player);
            context.TargetPosition = Optional<Vector3>.Some(player.position);

            return true;
        }
    }
}