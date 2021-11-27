using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private QuestSO quest;

        public void Give()
        {
            QuestManager.Instance.AddQuest(quest);
        }
    }
}