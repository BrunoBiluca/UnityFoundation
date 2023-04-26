using System;
using UnityEngine;

namespace UnityFoundation.DiceSystem
{
    [Serializable]
    public class MultiplierValue
    {
        [field: SerializeField] public int Amount { get; set; }
        [field: SerializeField] public float Multiplier { get; set; }

        public override string ToString()
        {
            return $"{Amount} x {Multiplier}";
        }
    }

    public class MultiplierDiceMono : AbstractDiceMono<MultiplierValue>
    {
    }
}