using System;

namespace UnityFoundation.Code
{
    public sealed partial class ValueEvaluation<T>
    {
        public sealed class Expression
        {
            private readonly ValueEvaluation<T> evaluate;

            public Func<T, bool> Condition { get; private set; }
            public Action Action { get; private set; }

            public Expression(ValueEvaluation<T> evaluate)
            {
                this.evaluate = evaluate;
            }

            public Expression If(Func<T, bool> condition)
            {
                Condition = condition;
                return this;
            }

            public ValueEvaluation<T> Do(Action action)
            {
                Action = action;
                return evaluate;
            }
        }

    }
}