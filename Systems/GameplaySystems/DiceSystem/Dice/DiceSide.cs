namespace UnityFoundation.DiceSystem
{

    public class DiceSide<TValue> : IDiceSide
    {
        private readonly int index;
        private readonly TValue value;

        public DiceSide(int index, TValue value)
        {
            this.index = index;
            this.value = value;
        }

        public int GetIndex() => index;

        public TValue GetValue() => value;

        public object GetValueObj() => value;
    }
}