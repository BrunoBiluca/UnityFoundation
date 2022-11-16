using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityFoundation.TurnSystem
{
    public class TurnSystem : ITurnSystem
    {
        public int CurrentTurn { get; private set; }

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
            CurrentTurn++;
            OnPlayerTurnEnded?.Invoke();
            OnEnemyTurnStarted?.Invoke();
        }

        public void EndEnemyTurn()
        {
            OnEnemyTurnEnded?.Invoke();
            OnPlayerTurnStarted?.Invoke();
        }
    }
}
