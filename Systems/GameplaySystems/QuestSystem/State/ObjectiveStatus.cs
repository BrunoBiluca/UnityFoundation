namespace Assets.UnityFoundation.Systems.QuestSystem
{
    public abstract class ObjectiveStatus
    {
        private readonly QuestObjectiveSO objective;

        public QuestObjectiveSO Objective => objective;

        public bool IsComplete { get; protected set; }

        protected ObjectiveStatus(QuestObjectiveSO objective)
        {
            this.objective = objective;
            IsComplete = false;
        }

        protected T GetObjective<T>() where T : QuestObjectiveSO => (T)objective;

        public abstract void UpdateObjectiveProgress(object parameters);
    }
}