using UnityEngine;

namespace UnityFoundation.Code.UnityAdapter
{
    public class ColorGenerator
    {
        public static Color Random()
        {
            return new Color(
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f),
                UnityEngine.Random.Range(0f, 1f)
            );
        }
    }
}
