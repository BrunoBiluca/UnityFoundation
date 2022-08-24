using UnityEngine;

namespace UnityFoundation.Editor.PrettyHierarchy
{
    public class PrettyObject : MonoBehaviour
    {
        [field: SerializeField] 
        public bool UseDefault { get; private set; } = true;

        [field: SerializeField] 
        public Color BackgroundColor { get; private set; } = Color.white;

        [field: SerializeField] 
        public Color FontColor { get; private set; } = Color.white;
    }
}