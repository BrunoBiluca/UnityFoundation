using UnityEngine;

namespace UnityFoundation.DiceSystem
{
    public class LogDiceEvaluate : ThrowDicesEventHandler<object>
    {
        public override void OnHandle(IDice dice, object value)
        {
            Debug.Log($"{dice}: {value}");
        }
    }
}