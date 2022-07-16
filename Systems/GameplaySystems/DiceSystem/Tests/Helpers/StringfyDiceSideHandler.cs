namespace UnityFoundation.DiceSystem.Tests
{
    public class StringfyDiceSideHandler : ThrowDicesEventHandler<object>
    {
        public string DiceSideValue { get; private set; }

        public override void OnHandle(IDice dice, object value)
        {
            DiceSideValue = value.ToString();
        }
    }
}