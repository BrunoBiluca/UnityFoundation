using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public class QuestReceiver : MonoBehaviour
    {
        [SerializeField] private QuestSO quest; public void TryFinishQuest()
        {
            QuestManager.Instance.TryFinishQuest(quest);
        }
    }
}
