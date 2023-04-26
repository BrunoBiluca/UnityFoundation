using System;
using System.Collections.Generic;

namespace UnityFoundation.DiceSystem
{
    public class ThrowDicesEvent
    {
        private bool startChecking;

        public event Action<DiceEvaluate> OnDiceEvaluate;
        public event Action<DiceEvaluate[]> OnDicesEvaluateFinish;
        public bool IsChecking => startChecking && Dices.Count > 0;
        public List<IDiceMono> Dices { get; private set; }

        public List<DiceEvaluate> dicesEvaluate;

        public ThrowDicesEvent(params IDiceMono[] dices)
        {
            startChecking = false;
            Dices = new List<IDiceMono>(dices);
            dicesEvaluate = new List<DiceEvaluate>();
        }

        public void Evaluate(IDiceMono dice, IDiceSide side)
        {
            if(!Dices.Remove(dice))
                return;

            var evaluate = new DiceEvaluate(dice, side);
            dicesEvaluate.Add(evaluate);
            OnDiceEvaluate?.Invoke(evaluate);

            if(Dices.Count == 0)
                OnDicesEvaluateFinish?.Invoke(dicesEvaluate.ToArray());
        }

        public void StartChecking()
        {
            startChecking = true;
        }

        public class DiceEvaluate
        {
            public IDiceMono Dice { get; private set; }
            public IDiceSide Side { get; private set; }

            public DiceEvaluate(IDiceMono dice, IDiceSide side)
            {
                Dice = dice;
                Side = side;
            }
        }
    }
}