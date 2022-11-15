using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityFoundation.Code.Timer;

namespace UnityFoundation.CarSystem
{
    public class LapManager : MonoBehaviour
    {
        private List<Checkpoint> checkpoints;

        private StopWatch lapStopwatch;
        private int checkpointCount;
        private bool isGameStarted;

        private void Awake()
        {
            lapStopwatch = new StopWatch();
            isGameStarted = false;
            checkpointCount = 0;
        }

        void Start()
        {
            checkpoints = GameObject.FindGameObjectsWithTag("check_point")
                .Select(go => go.GetComponent<Checkpoint>())
                .ToList();

            foreach(var checkpoint in checkpoints)
            {
                checkpoint.OnCheckpointPassed += OnCheckpointPassedHandler;
            }
        }

        private void Update()
        {
            if(!isGameStarted) return;
            CarProtoUI.Instance.UpdateLap(lapStopwatch.CurrentTime);
        }

        public void OnCheckpointPassedHandler()
        {
            checkpointCount++;

            if(!isGameStarted)
            {
                isGameStarted = true;
                lapStopwatch.Start();
                CarProtoUI.Instance.DisplayGameStartedText();
            }

            if(checkpointCount == checkpoints.Count + 1)
            {
                checkpointCount = 1;
                lapStopwatch.Lap();
                CarProtoUI.Instance.UpdateBestLap(lapStopwatch.BestLap);
            }

            CarProtoUI.Instance.UpdateCheckpoint(checkpointCount);
        }
    }
}