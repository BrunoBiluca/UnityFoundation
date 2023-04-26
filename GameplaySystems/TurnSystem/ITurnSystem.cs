using System;

namespace UnityFoundation.TurnSystem
{
    public interface ITurnSystem
    {
        int CurrentTurn { get; }

        event Action OnPlayerTurnStarted;
        event Action OnPlayerTurnEnded;

        event Action OnEnemyTurnStarted;
        event Action OnEnemyTurnEnded;

        void EndEnemyTurn();
        void EndPlayerTurn();
    }
}
