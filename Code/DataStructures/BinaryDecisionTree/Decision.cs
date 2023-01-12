using System;

namespace UnityFoundation.Code
{
    public sealed class Decision : IDecision
    {
        private readonly Func<bool> handler;

        public bool WasSuccessful { get; private set; }

        public Decision(Func<bool> handler)
        {
            this.handler = handler;
        }

        public void Decide()
        {
            if(handler())
                WasSuccessful = true;
        }
    }
}
