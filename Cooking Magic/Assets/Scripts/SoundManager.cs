using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundEffsSO soundEffsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(soundEffsSO.deliverySuccess, Camera.main.transform.position);
    }
    
    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(soundEffsSO.deliveryFailure, Camera.main.transform.position);
    }


    private void PlaySound(AudioClip[] soundEffectArray, Vector3 position, float volume = 1.0f)
    {
        AudioClip soundEffect = soundEffectArray[Random.Range(0, soundEffectArray.Length)];
        AudioSource.PlayClipAtPoint(soundEffect, position, volume);
    }

    private void PlaySound(AudioClip soundEffect, Vector3 position, float volume = 1.0f)
    {
        AudioSource.PlayClipAtPoint(soundEffect, position, volume);
    }
    

}
