using System;
using System.Collections.Generic;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.DiceSystem
{
    public class DiceRoll
    {
        private readonly IRandomGenerator random;
        private readonly List<IDice> dices;

        public DiceRoll(
            IRandomGenerator random
        )
        {
            this.random = random;
            dices = new List<IDice>();
        }

        public int AddDice(IDice dice)
        {
            dices.Add(dice);
            return dices.Count - 1;
        }

        public int Roll(int diceIndex)
        {
            if(dices[diceIndex].SidesCount == 0)
                throw new InvalidOperationException();

            return random.Range(0, dices[diceIndex].SidesCount);
        }

        public int[] RollAll()
        {
            var results = new int[dices.Count];
            for(int index = 0; index < dices.Count; index++)
            {
                results.SetValue(Roll(index), index);
            }
            return results;
        }
    }
}