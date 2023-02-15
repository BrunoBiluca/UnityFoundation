using UnityEngine;

namespace UnityFoundation.Code
{
    public class LerpByTime
    {
        public float StartValue { get; }
        public float EndValue { get; }
        public float CurrentValue { get; private set; }
        public float CurrentTime { get; private set; }
        public float Time { get; }

        public LerpByTime(float startValue, float endValue, float time)
        {
            StartValue = startValue;
            CurrentValue = startValue;
            EndValue = endValue;
            Time = time;
        }

        public void Eval(float timeAmount)
        {
            CurrentTime += timeAmount;
            CurrentValue = Mathf.Lerp(
                StartValue, 
                EndValue, 
                CurrentTime / Time
            );
        }
    }
}