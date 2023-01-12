using System;
using System.Collections.Generic;

namespace UnityFoundation.Code
{
    /// <summary>
    /// Class responsible to evaluate simple expressions.
    /// Should be used to hold references for evaluations that will be used over the same code.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed partial class ValueEvaluation<T>
    {
        public static ValueEvaluation<T> Create(Func<T> callback)
        {
            return new ValueEvaluation<T>(callback);
        }

        private readonly List<Expression> expressions;

        public Func<T> Callback { get; }

        public ValueEvaluation()
        {
            expressions = new List<Expression>();
        }

        public ValueEvaluation(Func<T> callback) : this()
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