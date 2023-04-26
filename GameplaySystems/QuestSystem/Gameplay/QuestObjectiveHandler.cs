using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public class QuestObjectiveHandler : MonoBehaviour
    {
        [SerializeField] private QuestSO quest;
        [SerializeField] private List<QuestObjectiveSO> objetives;

        public void UpdateObjetiveProgress(object parameters)
        {
            foreach(var objetive in objetives)
            {
                QuestManager.Instance.UpdateObjetiveProgress(quest, objetive, parameters);
            }
        }
    }
}