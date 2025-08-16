using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

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
        // Do nothing
    }
}
