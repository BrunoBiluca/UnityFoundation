using System;

namespace UnityFoundation.Code
{
    public class BinaryDecision<TContext> : BaseDecisionHandler<TContext>
    {
        private readonly Func<TContext, bool> callback;
        public bool WasSuccessful { get; set; }


        public BinaryDecision(Func<TContext, bool> callback)
        {
            this.callback = callback;
        }

        protected override bool OnDecide(TContext context)
        {
            WasSuccessful = false;

            if(callback == null)
                return false;

            WasSuccessful = callback(context);
            return WasSuccessful;
        }
    }
}