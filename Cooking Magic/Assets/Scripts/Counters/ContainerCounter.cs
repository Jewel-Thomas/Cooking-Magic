using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public event EventHandler OnPlayerGrabObject;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObjects(kitchenObjectsSO, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    } 
    
    public override void InteractAlternate(Player player)
    {
        // Do nothing
    }
}
