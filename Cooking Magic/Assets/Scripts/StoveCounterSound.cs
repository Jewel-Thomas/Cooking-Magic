using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private StoveCounter stoveCounter;

    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(SoundManager.PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.7f);
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float playWarningSoundProgress = 0.5f;
        playWarningSound = stoveCounter.isFried() && e.progressNormalized > playWarningSoundProgress;
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

    private void Update()
    {
        if(playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if(warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayBurnWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
