using System.Collections.Generic;
using System.Linq;

namespace UnityFoundation.DiceSystem
{
    public class Dice<TValue> : IDice
    {
        private readonly List<DiceSide<TValue>> sides;
        public int SidesCount => sides.Count;
        public IDiceSide[] Sides => sides.ToArray();

        public Dice()
        {
            sides = new List<DiceSide<TValue>>();
        }

        public void AddSide(TValue diceSide)
        {
            sides.Add(new DiceSide<TValue>(SidesCount, diceSide));
        }

        public DiceSide<TValue> GetSide(int sideIndex)
        {
            return sides.Where(s => s.GetIndex() == sideIndex).FirstOrDefault();
        }
    }
}