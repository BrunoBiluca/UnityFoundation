using System;

namespace UnityFoundation.Code
{
    public sealed partial class Evaluation<T>
    {
        public sealed class Expression
        {
            private readonly Evaluation<T> evaluate;

            public Func<T, bool> Condition { get; private set; }
            public Action Action { get; private set; }

            public Expression(Evaluation<T> evaluate)
            {
                this.evaluate = evaluate;
            }

            public Expression If(Func<T, bool> condition)
            {
                Condition = condition;
                return this;
            }

            public Evaluation<T> Do(Action action)
            {
                Action = action;
                return evaluate;
            }
        }

    }
}