using UnityEngine;

namespace UnityFoundation.Code
{
    /// <summary>
    /// Linear interpolation between two values given an especific time
    /// </summary>
    public class LerpByTime
    {
        public float StartValue { get; }
        public float EndValue { get; }
        public float CurrentValue { get; private set; }
        public float CurrentTime { get; private set; }
        public float Time { get; }

        public bool Ended => CurrentTime >= Time;
        public bool Looping { get; set; } = false;

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

            if(Looping)
                if(CurrentTime >= Time)
                    CurrentTime = 0;

            CurrentValue = Mathf.Lerp(
                StartValue,
                EndValue,
                CurrentTime / Time
            );
        }
    }
}