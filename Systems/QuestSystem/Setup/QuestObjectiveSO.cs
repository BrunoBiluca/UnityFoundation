using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public abstract class QuestObjectiveSO : ScriptableObject
    {
        [SerializeField] private string description;

        public string Description => description;

        [SerializeField] private bool isRequired;

        public bool IsRequired => isRequired;

        public abstract ObjectiveStatus Initiate();
    }
}