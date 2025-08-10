using UnityEngine;
public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // It does not have kitchen object
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Nothing needs to be done
            }
        }
        else
        {
            // It does have kitchen object
            if (player.HasKitchenObject())
            {
                // Nothing needs to be done
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObjects(cutKitchenObjectSO, this);
        }
    }
}
