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
