using UnityFoundation.Code;

namespace UnityFoundation.Zombies
{
    public class ResetStateHandler : BaseDecisionHandler<SimpleBrainContext>
    {
        public override bool OnHandle(SimpleBrainContext context)
        {
            context.ResetStates();
            return true;
        }
    }
}