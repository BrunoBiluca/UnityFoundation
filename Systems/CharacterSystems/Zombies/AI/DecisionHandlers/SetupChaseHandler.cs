using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.Zombies
{
    public class SetupChaseHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        private readonly Transform player;

        public SetupChaseHandler(Transform player)
        {
            this.player = player;
        }

        public override bool OnHandle(SimpleBrainContext context)
        {
            context.IsChasing = true;
            context.IsRunning = true;
            context.TargetPosition = Optional<Vector3>.Some(player.transform.position);

            return true;
        }
    }
}