using UnityFoundation.Code;

namespace UnityFoundation.Zombies
{
    public class ResetStateHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        protected override bool OnDecide(SimpleBrainContext context)
        {
            context.ResetStates();
            return true;
        }
    }
}