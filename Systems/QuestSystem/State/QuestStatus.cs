using System;
using System.Collections.Generic;
using System.Linq;
using Assets.UnityFoundation.Code.Common;

namespace Assets.UnityFoundation.Systems.QuestSystem
{
    [Serializable]
    public class QuestStatus
    {
        public QuestSO Quest { get; private set; }

        private List<ObjectiveStatus> objectives;

        public List<ObjectiveStatus> ObjetivesStatus => objectives;
        public int Progress => objectives.FindAll(o => o.IsComplete).Count;
        public bool IsComplete => objectives
            .All(o => !o.Objective.IsRequired || o.IsComplete);

        public QuestStatus(QuestSO questSO)
        {
            Quest = questSO;

            objectives = new List<ObjectiveStatus>();
            foreach(var objectiveSO in questSO.Objectives)
            {
                objectives.Add(objectiveSO.Initiate());
            }
        }

        public void UpdateObjetivProgress(QuestObjectiveSO objetiveSO, object parameters)
        {
            objectives.Find(o => o.Objective == objetiveSO)
                .UpdateObjectiveProgress(parameters);
        }
    }
}