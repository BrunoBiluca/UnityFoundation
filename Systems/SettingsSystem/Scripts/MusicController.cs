using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    [SerializeField] private SettingsOptionSO musicSettings;
    private AudioSource backgroundMusicAudioSource;

    private float musicVolume;

    void Awake()
    {
        backgroundMusicAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SettingsSaver.Instance.Load(musicSettings, out musicVolume);

        musicSettings.OnChangeValue += (sender, args) => {
            SettingsSaver.Instance.Load(musicSettings, out musicVolume);
            PlayOrTurnOffMusic();
        };

        PlayOrTurnOffMusic();
    }

    void PlayOrTurnOffMusic()
    {
        backgroundMusicAudioSource.volume = musicVolume;

        if(musicVolume == 0 && backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Stop();
            return;
        }

        if(backgroundMusicAudioSource.volume > 0 && !backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Play();
        }        
    }
}
