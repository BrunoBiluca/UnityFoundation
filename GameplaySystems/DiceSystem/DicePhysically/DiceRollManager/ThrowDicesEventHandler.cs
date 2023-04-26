namespace UnityFoundation.DiceSystem
{
    public abstract class ThrowDicesEventHandler<T>
    {
        private T GetValue(IDiceSide side)
        {
            return (T)side.GetValueObj();
        }

        public void Handle(ThrowDicesEvent.DiceEvaluate diceEvaluate)
        {
            OnHandle(diceEvaluate.Dice, GetValue(diceEvaluate.Side));
        }

        public abstract void OnHandle(IDice dice, T value);
    }
}