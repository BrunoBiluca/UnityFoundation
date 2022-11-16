using UnityEngine;

namespace UnityFoundation.Code
{
    public class PrettyObjectMono : MonoBehaviour, IPrettyable
    {
        [field: SerializeField]
        public bool UseDefault { get; private set; } = true;

        [field: SerializeField]
        public Color BackgroundColor { get; private set; } = Color.white;

        [field: SerializeField]
        public Color FontColor { get; private set; } = Color.white;

        public PrettyObject BePretty()
        {
            return new PrettyObject(UseDefault, BackgroundColor, FontColor, gameObject);
        }
    }
}