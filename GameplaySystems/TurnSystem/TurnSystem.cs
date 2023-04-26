using System;
using UnityFoundation.Code.DebugHelper;

namespace UnityFoundation.TurnSystem
{
    public class TurnSystem : ITurnSystem, IBilucaLoggable
    {
        public int CurrentTurn { get; private set; }
        public IBilucaLogger Logger { get; set; }

        public event Action OnPlayerTurnStarted;
        public event Action OnPlayerTurnEnded;

        public event Action OnEnemyTurnStarted;
        public event Action OnEnemyTurnEnded;

        public TurnSystem()
        {
            CurrentTurn = 1;
        }

        public void EndPlayerTurn()
        {
            Logger?.LogHighlight("End player turn", CurrentTurn.ToString());
            CurrentTurn++;
            OnPlayerTurnEnded?.Invoke();
            OnEnemyTurnStarted?.Invoke();
        }

        public void EndEnemyTurn()
        {
            Logger?.LogHighlight("End enemy turn", CurrentTurn.ToString());
            OnEnemyTurnEnded?.Invoke();
            OnPlayerTurnStarted?.Invoke();
        }
    }
}
