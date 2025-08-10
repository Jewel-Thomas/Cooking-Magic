using UnityEngine;
public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // It does not have kitchen object
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectsSO()))
        {
            KitchenObjectsSO outputKitchenObject = GetOutputSlice(GetKitchenObject().GetKitchenObjectsSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObjects(outputKitchenObject, this);
        }
    }

    private KitchenObjectsSO GetOutputSlice(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (CuttingRecipeSO cutKitchenObjectSO in cutKitchenObjectSOArray)
        {
            if (cutKitchenObjectSO.input == kitchenObjectsSO)
            {
                return cutKitchenObjectSO.output;
            }
        }

        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (CuttingRecipeSO cutKitchenObjectSO in cutKitchenObjectSOArray)
        {
            if (cutKitchenObjectSO.input == kitchenObjectsSO)
            {
                return true;
            }
        }

        return false;
    }
}
