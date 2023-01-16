using System;
using System.Threading.Tasks;

namespace UnityFoundation.Code
{
    public interface ICondition
    {
        bool Resolve();
    }

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

    public class LoopConditionAsync
    {
        public static LoopConditionAsync While(ICondition condition, int delay = 25)
        {
            return new LoopConditionAsync(condition) {
                Delay = delay
            };
        }

        public static LoopConditionAsync While(Func<bool> callback, int delay = 25)
        {
            return new LoopConditionAsync(Condition.Create(callback)) {
                Delay = delay
            };
        }

        private readonly ICondition conditionCallback;

        private LoopConditionAsync() { }

        public int Delay { get; set; } = 25;

        private LoopConditionAsync(ICondition conditionCallback)
        {
            this.conditionCallback = conditionCallback;
        }

        public async Task Loop(Action action)
        {
            while(conditionCallback.Resolve())
            {
                action();
                await Task.Delay(Delay);
            }
        }
    }
}
