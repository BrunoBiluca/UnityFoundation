using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.Zombies
{
    public class CanAttackHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        private readonly Transform player;

        public CanAttackHandler(Transform player)
        {
            this.player = player;
        }

        public override bool OnHandle(SimpleBrainContext context)
        {
            if(player == null)
                return false;

            if(Time.time < context.NextAttackTime)
                return false;

            return true;
        }
    }
}