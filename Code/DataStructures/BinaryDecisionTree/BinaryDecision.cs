using System;

namespace UnityFoundation.Code
{
    public sealed class BinaryDecision : IBinaryDecision
    {
        public bool WasSuccessful { get; private set; }

        private readonly Func<bool> curr;
        private IBinaryDecision next;
        private IBinaryDecision failed;
        private IDecision final;

        public BinaryDecision(Func<bool> handler)
        {
            curr = handler;
        }

        public IBinaryDecision SetNext(IBinaryDecision next)
        {
            this.next = next;
            return this;
        }

        public IBinaryDecision SetFailed(IBinaryDecision failed)
        {
            this.failed = failed;
            return this;
        }

        public IBinaryDecision SetFinal(IDecision final)
        {
            this.final = final;
            return this;
        }

        public void Decide()
        {
            if(curr())
            {
                WasSuccessful = true;

                if(final != null)
                {
                    final.Decide();
                    return;
                }

                next?.Decide();
            }
            else
            {
                failed?.Decide();
            }
        }
    }
}
