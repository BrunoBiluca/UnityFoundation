using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Zombies
{
    public class PlayerRangeHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        private readonly float minDistance;
        private readonly Transform player;

        public PlayerRangeHandler(float minDistance, Transform player)
        {
            this.minDistance = minDistance;
            this.player = player;
        }

        protected override bool OnDecide(SimpleBrainContext context)
        {
            var distance = Vector3.Distance(context.Body.position, player.position);
            return distance <= minDistance;
        }
    }
}