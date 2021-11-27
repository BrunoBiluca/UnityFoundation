using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    [CreateAssetMenu(menuName = "QuestSystem/Quest")]
    public class QuestSO : ScriptableObject
    {
        [SerializeField] private string title;
        public string Title => title;

        [Tooltip(
            "Finish quest when all objetives are fulfilled automatically"
            + "\n"
            + "Uncheck this field to finish manually"
        )]
        [SerializeField] private bool autoFinish;

        public bool AutoFinish => autoFinish;

        [SerializeField] private List<QuestObjectiveSO> objectives;
        public List<QuestObjectiveSO> Objectives => objectives;
        public int TotalObjectives => objectives.Count;

        [SerializeField] private string rewards;
        public string Rewards => rewards;

    }
}