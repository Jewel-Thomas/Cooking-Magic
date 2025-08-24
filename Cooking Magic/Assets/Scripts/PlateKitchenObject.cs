using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }
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
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectsSO = kitchenObjectsSO
            });
            return true;
        }
    }

    public List<KitchenObjectsSO> GetIngredientList()
    {
        return ingredientList;
    }
}
