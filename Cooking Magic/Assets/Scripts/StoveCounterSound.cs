using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(SoundManager.PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.7f);
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
    }

    private void SoundManager_OnVolumeChanged(object sender, SoundManager.OnVolumeChangedEventArgs e)
    {
        audioSource.volume = e.volume;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
