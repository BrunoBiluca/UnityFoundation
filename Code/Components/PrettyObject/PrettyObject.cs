using UnityEngine;

namespace UnityFoundation.Code
{
    public class PrettyObject
    {
        public bool UseDefault { get; }
        public Color BackgroundColor { get; }
        public Color FontColor { get; }
        public GameObject GameObject { get; }

        public PrettyObject(
            bool useDefault,
            Color backgroundColor,
            Color fontColor,
            GameObject gameObject
        )
        {
            UseDefault = useDefault;
            BackgroundColor = backgroundColor;
            FontColor = fontColor;
            GameObject = gameObject;
        }
    }
}