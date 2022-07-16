namespace UnityFoundation.DiceSystem
{
    public interface IDice
    {
        public int SidesCount { get; }
        public IDiceSide[] Sides { get; }
    }
}