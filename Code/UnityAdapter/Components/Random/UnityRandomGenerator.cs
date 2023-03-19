using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class UnityRandomGenerator : IRandomGenerator
    {
        public int Next(int maxValue)
        {
            return Random.Range(0, maxValue);
        }

        public int Range(int minInclusive, int maxExclusive)
        {
            return Random.Range(minInclusive, maxExclusive);
        }
    }
}