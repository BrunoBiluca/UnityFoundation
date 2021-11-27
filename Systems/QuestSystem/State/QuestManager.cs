using Assets.UnityFoundation.Code.Common;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public class QuestManager : Singleton<QuestManager>
    {
        [SerializeField] private QuestlListUI questListUI;

        public QuestStatusList QuestList { get; private set; }

        protected override void OnAwake()
        {
            QuestList = GetComponent<QuestStatusList>();
            QuestList.OnUpdate += () => questListUI.Display(QuestList.QuestStatuses);
        }

        public void AddQuest(QuestSO quest)
        {
            QuestList.Add(quest);
        }
        public void UpdateObjetiveProgress(
            QuestSO quest,
            QuestObjectiveSO objetive,
            object parameters
        )
        {
            QuestList.UpdateObjetiveProgress(quest, objetive, parameters);
        }

        public bool TryFinishQuest(QuestSO quest)
        {
            return QuestList.TryFinish(quest);
        }
    }
}