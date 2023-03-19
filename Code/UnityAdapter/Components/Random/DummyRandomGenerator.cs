using System;

namespace UnityFoundation.Code.UnityAdapter
{
    public class DummyRandomGenerator : IRandomGenerator
    {
        private readonly int[] dummyValues;
        private int valueIndex;

        public DummyRandomGenerator(params int[] dummyValues)
        {
            valueIndex = 0;
            this.dummyValues = dummyValues;
        }

        public int Next(int maxValue)
        {
            throw new NotImplementedException();
        }

        public int Range(int inclusiveMin, int exclusiveMax)
        {
            if(valueIndex >= dummyValues.Length)
                valueIndex = 0;

            var dummyValue = dummyValues[valueIndex++];

            if(dummyValue < inclusiveMin || dummyValue >= exclusiveMax)
                throw new ArgumentOutOfRangeException(@$"
                    {dummyValue} is not in range for this call [{inclusiveMin}, {exclusiveMax}].
                    All values [{string.Join(", ", dummyValues)}]
                ");

            return dummyValue;
        }
    }
}