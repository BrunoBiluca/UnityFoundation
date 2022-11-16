using System;
using System.Collections.Generic;

namespace UnityFoundation.Code
{
    public sealed partial class Evaluation<T>
    {
        public static Evaluation<T> Create(Func<T> callback)
        {
            return new Evaluation<T>(callback);
        }

        private readonly List<Expression> expressions;

        public Func<T> Callback { get; }

        public Evaluation()
        {
            expressions = new List<Expression>();
        }

        public Evaluation(Func<T> callback) : this()
        {
            Callback = callback;
        }

        public Expression If(Func<T, bool> condition)
        {
            var newExpression = new Expression(this).If(condition);
            expressions.Add(newExpression);
            return newExpression;
        }

        public void Eval()
        {
            if(Callback == null)
                throw new NullReferenceException("callback was not set by an instance");
            Eval(Callback());
        }

        public void Eval(T value)
        {
            foreach(var expr in expressions)
            {
                if(expr.Condition(value))
                {
                    expr.Action();
                    return;
                }
            }
        }
    }
}