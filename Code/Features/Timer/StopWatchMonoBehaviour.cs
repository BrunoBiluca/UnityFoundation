using UnityEngine;

namespace UnityFoundation.Code.Timer
{
    public class StopWatchMonoBehaviour : MonoBehaviour
    {

        public float CurrentTime => time;

        private float time;

        public StopWatchMonoBehaviour Setup()
        {
            time = 0f;
            return this;
        }

        private void Update()
        {
            time += Time.deltaTime;
        }
    }
}