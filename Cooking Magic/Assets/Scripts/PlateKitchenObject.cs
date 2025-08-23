using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectsSO> ingredientList;
    [SerializeField] private List<KitchenObjectsSO> validIngredients;

    private void Awake()
    {
        ingredientList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO)
    {
        if (!validIngredients.Contains(kitchenObjectsSO))
        {
            return false;
        }
        if (ingredientList.Contains(kitchenObjectsSO))
            {
                return false;
            }
            else
            {
                ingredientList.Add(kitchenObjectsSO);
                return true;
            }
    }
}
