using UnityFoundation.Code;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public class QuestStatusList : MonoBehaviour
    {
        private List<QuestStatus> questStatuses;
        public List<QuestStatus> QuestStatuses {
            get {
                if(questStatuses == null)
                    questStatuses = new List<QuestStatus>();

                return questStatuses;
            }
        }

        public event Action OnUpdate;
        public event Action<QuestStatus> OnQuestFinished;

        public void Add(QuestSO quest)
        {
            QuestStatuses.AddIfNot(
                qs => qs.Quest == quest,
                new QuestStatus(quest)
            );

            OnUpdate?.Invoke();
        }

        public void UpdateObjetiveProgress(
            QuestSO quest,
            QuestObjectiveSO objetive,
            object parameters
        )
        {
            GetStatusBy(quest)
                .Some(status => {
                    status.UpdateObjetivProgress(objetive, parameters);

                    if(status.Quest.AutoFinish && status.IsComplete)
                    {
                        questStatuses.Remove(status);
                        OnQuestFinished?.Invoke(status);
                    }

                    OnUpdate?.Invoke();
                });
        }

        public bool TryFinish(QuestSO quest)
        {
            if(!GetStatusBy(quest).IsPresentAndGet(out QuestStatus status))
                return false;

            if(!status.IsComplete)
                return false;

            questStatuses.Remove(status);
            OnUpdate?.Invoke();
            OnQuestFinished?.Invoke(status);
            return true;
        }

        public Optional<QuestStatus> GetStatusBy(QuestSO quest)
        {
            var questStatus = QuestStatuses.Find(qs => qs.Quest == quest);

            if(questStatus == null)
                return Optional<QuestStatus>.None();

            return Optional<QuestStatus>.Some(questStatus);
        }
    }
}