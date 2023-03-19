namespace UnityFoundation.Code.UnityAdapter
{
    public interface IRandomGenerator
    {
        int Next(int maxExclusive);
        int Range(int minInclusive, int maxExclusive);
    }
}