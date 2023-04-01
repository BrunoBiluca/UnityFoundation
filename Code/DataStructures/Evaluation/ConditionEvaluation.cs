using System;

namespace UnityFoundation.Code
{
    public class ConditionEvaluation : ICondition
    {
        public static ConditionEvaluation Create(Func<bool> callback)
        {
            return new ConditionEvaluation(callback);
        }

        private readonly Func<bool> callback;

        public ConditionEvaluation(Func<bool> callback)
        {
            this.callback = callback;
        }

        public bool Resolve() => callback();
    }
}
