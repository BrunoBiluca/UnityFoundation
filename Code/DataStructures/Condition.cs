using System;

namespace UnityFoundation.Code
{
    public class Condition : ICondition
    {
        public static Condition Create(Func<bool> callback)
        {
            return new Condition(callback);
        }

        private readonly Func<bool> callback;

        public Condition(Func<bool> callback)
        {
            this.callback = callback;
        }

        public bool Resolve() => callback();
    }
}
