using System;
using UnityEngine;
public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSOArray;
    private int cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // It does not have kitchen object
            if (player.HasKitchenObject())
            {
                // The player is carrying something that can be cut
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectsSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectsSO());
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        cuttingProgressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMaxValue
                    });
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
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(GetKitchenObject().GetKitchenObjectsSO());

            OnCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                cuttingProgressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMaxValue
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMaxValue)
            {
                KitchenObjectsSO outputKitchenObject = GetOutputSlice(GetKitchenObject().GetKitchenObjectsSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObjects(outputKitchenObject, this);
            }
        }
    }

    private KitchenObjectsSO GetOutputSlice(KitchenObjectsSO inputkitchenObjectsSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(inputkitchenObjectsSO);
        if (cuttingRecipeSO)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputkitchenObjectsSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOFromInput(inputkitchenObjectsSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOFromInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cutKitchenObjectSO in cutKitchenObjectSOArray)
        {
            if (cutKitchenObjectSO.input == inputKitchenObjectSO)
            {
                return cutKitchenObjectSO;
            }
        }

        return null;
    }
}
