using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectsSO plateKitchenOjectSO;
    private float currentspawnTime;
    private float spawnTimeMax = 4f;
    private int currentPlateAmount;
    private int maxPlateAmount = 4;
    private void Update()
    {
        currentspawnTime += Time.deltaTime;
        if (currentspawnTime > spawnTimeMax)
        {
            currentspawnTime = 0f;
            if (currentPlateAmount < maxPlateAmount)
            {
                currentPlateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (currentPlateAmount > 0)
            {
                currentPlateAmount--;
                KitchenObject.SpawnKitchenObjects(plateKitchenOjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
