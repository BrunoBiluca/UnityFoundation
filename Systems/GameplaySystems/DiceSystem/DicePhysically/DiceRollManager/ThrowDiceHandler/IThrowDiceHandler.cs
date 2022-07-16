namespace UnityFoundation.DiceSystem
{
    public interface IThrowDiceHandler
    {
        public void Handle(ThrowDicesEvent throwEvent);
    }
}