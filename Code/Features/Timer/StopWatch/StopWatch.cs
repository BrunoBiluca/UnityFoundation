using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityFoundation.Code.Timer
{
    public class StopWatch
    {
        private string name;

        private List<float> laps;

        private StopWatchMonoBehaviour stopWatchMonoBehaviour;

        public float CurrentTime {
            get {
                if(stopWatchMonoBehaviour == null)
                    return 0f;

                return stopWatchMonoBehaviour.CurrentTime;
            }
        }

        public float BestLap {
            get {
                if(laps.Count == 0)
                    return 0f;

                return laps.Min();
            }
        }

        public StopWatch()
        {
            laps = new List<float>();
        }

        public StopWatch SetName(string newName)
        {
            name = newName;
            return this;
        }

        public void Start()
        {
            if(stopWatchMonoBehaviour == null)
            {
                var goName = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString() : name;
                stopWatchMonoBehaviour = new GameObject(goName)
                    .AddComponent<StopWatchMonoBehaviour>();
            }

            stopWatchMonoBehaviour.Setup();
        }

        public void Lap()
        {
            if(stopWatchMonoBehaviour == null) return;

            laps.Add(stopWatchMonoBehaviour.CurrentTime);
            stopWatchMonoBehaviour.Setup();
        }

        public void RestartLaps()
        {
            laps = new List<float>();
        }
    }
}