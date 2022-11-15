using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class UnityRandomGenerator : IRandomGenerator
    {
        public int Range(int minInclusive, int maxExclusive)
        {
            return Random.Range(minInclusive, maxExclusive);
        }
    }
}