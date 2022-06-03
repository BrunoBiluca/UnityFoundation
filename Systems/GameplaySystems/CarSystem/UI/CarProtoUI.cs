using UnityFoundation.Code;
using TMPro;
using UnityEngine;
using UnityFoundation.Tools.TimeUtils;

namespace UnityFoundation.CarSystem
{
    public class CarProtoUI : Singleton<CarProtoUI>
    {
        private GameObject gameStartedText;
        private TextMeshProUGUI checkPointsText;
        private TextMeshProUGUI lapText;
        private TextMeshProUGUI bestLapText;

        private Timer gameStartedTimer;

        protected override void OnAwake()
        {
            gameStartedText = transform.Find("game_started_text").gameObject;
            checkPointsText = transform.FindComponent<TextMeshProUGUI>("checkpoint_text");
            lapText = transform.FindComponent<TextMeshProUGUI>("lap_time_text");
            bestLapText = transform.FindComponent<TextMeshProUGUI>("best_lap_time_text");

            gameStartedTimer = new Timer(3f, () => gameStartedText.gameObject.SetActive(false));
            gameStartedText.SetActive(false);
        }

        public void DisplayGameStartedText()
        {
            gameStartedText.SetActive(true);
            gameStartedTimer.Start();
        }

        public void UpdateCheckpoint(int checkPoints)
        {
            checkPointsText.text = $"Checkpoints: {checkPoints.ToString("00")}";
        }

        public void UpdateBestLap(float bestLapTime)
        {
            bestLapText.text = $"Best Lap: {bestLapTime.ToString("00:00")}";
        }

        public void UpdateLap(float currentTime)
        {
            lapText.text = $"Lap: {currentTime.ToString("00:00")}";
        }
    }
}