using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.SettingsSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicControllerMono : MonoBehaviour, IMusicController
    {
        [SerializeField] private MusicController.Settings musicSettings;
        [SerializeField] private AudioSettingsOptionSO audioSettings;
        IMusicController musicController;

        public IAudioSource AudioSource { get; private set; }

        public void Awake()
        {
            AudioSource = GetComponent<AudioSource>().Decorate();
            Setup(audioSettings.Value, musicSettings);
            Play();
        }

        public void Setup(
            IAudioSettings audioSettings,
            MusicController.Settings musicSettings
        )
        {
            musicController = new MusicController(AudioSource, audioSettings, musicSettings);
        }

        public void Play() => musicController.Play();

        private void OnDestroy() => Stop();

        public void Stop() => musicController.Stop();
    }
}
