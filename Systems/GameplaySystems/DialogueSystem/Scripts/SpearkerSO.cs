using UnityEngine;

namespace Assets.UnityFoundation.DialogueSystem
{
    [CreateAssetMenu(fileName = "New speaker", menuName = "Dialogue/Spearker")]
    public class SpearkerSO : ScriptableObject
    {
        [SerializeField] private string spearkerName;
        [SerializeField] private Color editorNameColor = Color.white;

        public string SpearkerName { get { return spearkerName; } }
        public Color EditorNameColor { get { return editorNameColor; } }
    }
}