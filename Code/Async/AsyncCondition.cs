using System;
using System.Threading.Tasks;

namespace UnityFoundation.Code
{

    public class AsyncCondition
    {
        public static AsyncCondition While(ICondition condition, int delay = 25)
        {
            return new AsyncCondition(condition) {
                Delay = delay
            };
        }

        public static AsyncCondition While(Func<bool> callback, int delay = 25)
        {
            return new AsyncCondition(ConditionEvaluation.Create(callback)) {
                Delay = delay
            };
        }

        private readonly ICondition conditionCallback;

        private AsyncCondition() { }

        public int Delay { get; set; } = 25;

        private AsyncCondition(ICondition conditionCallback)
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
