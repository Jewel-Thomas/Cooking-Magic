using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }


    public event EventHandler<OnVolumeChangedEventArgs> OnVolumeChanged;
    public class OnVolumeChangedEventArgs: EventArgs
    {
        public float volume;
    }

    [SerializeField] private SoundEffsSO soundEffsSO;

    private float volume = 0.7f;

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.7f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnItemPicked += Player_OnItemPicked;
        BaseCounter.OnAnyItemDropped += BaseCounter_OnAnyItemDropped;
        TrashCounter.OnAnyItemTrashed += TrashCounter_OnAnyItemTrashed;
    }

    private void TrashCounter_OnAnyItemTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(soundEffsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyItemDropped(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundEffsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnItemPicked(object sender, EventArgs e)
    {
        PlaySound(soundEffsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundEffsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundEffsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundEffsSO.deliveryFailure, deliveryCounter.transform.position);
    }


    private void PlaySound(AudioClip[] soundEffectArray, Vector3 position, float volumeMultiplier = 1.0f)
    {
        AudioClip soundEffect = soundEffectArray[Random.Range(0, soundEffectArray.Length)];
        PlaySound(soundEffect, position, volumeMultiplier);
    }

    private void PlaySound(AudioClip soundEffect, Vector3 position, float volumeMultiplier = 1.0f)
    {
        AudioSource.PlayClipAtPoint(soundEffect, position, volumeMultiplier * volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume)
    {
        PlaySound(soundEffsSO.footstep, position, volume);
    }

    public void PlayCountDownSound()
    {
        PlaySound(soundEffsSO.warning[0], Vector3.zero);
    }

    public void PlayBurnWarningSound(Vector3 position)
    {
        PlaySound(soundEffsSO.warning, position);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if(volume > 1.0f)
        {
            volume = 0.0f;
        }

        OnVolumeChanged?.Invoke(this, new OnVolumeChangedEventArgs
        {
            volume = volume
        });

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }

}
