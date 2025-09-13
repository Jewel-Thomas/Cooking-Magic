using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeList;
    private float recipeListTimer = 0f;
    private float recipeListTimerMax = 4f;
    private int recipeItemsMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }

    private void Update()
    {
        recipeListTimer -= Time.deltaTime;
        if (recipeListTimer <= 0)
        {
            recipeListTimer = recipeListTimerMax;

            if (waitingRecipeList.Count < recipeItemsMax)
            {
                RecipeSO recipeSO = recipeListSO.recipeList[Random.Range(0, recipeListSO.recipeList.Count)];
                Debug.Log("Recipe" + recipeSO.recipeName);
                waitingRecipeList.Add(recipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO recipeSO = waitingRecipeList[i];

            if (recipeSO.ingredientList.Count == plateKitchenObject.GetIngredientList().Count)
            {
                bool plateContentMatched = true;
                foreach (KitchenObjectsSO plateKitchenObjectSO in plateKitchenObject.GetIngredientList())
                {
                    if (!recipeSO.ingredientList.Contains(plateKitchenObjectSO))
                    {
                        plateContentMatched = false;
                        break;
                    }
                }
                if (plateContentMatched)
                {
                    Debug.Log("Player Delivered the correct Recipe!");
                    waitingRecipeList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        Debug.Log("Player did not deliver the right recipe!");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeList;
    }
}
