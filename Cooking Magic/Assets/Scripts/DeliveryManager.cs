using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
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

            if (waitingRecipeList.Count <= recipeItemsMax)
            {
                RecipeSO recipeSO = recipeListSO.recipeList[Random.Range(0, recipeListSO.recipeList.Count)];
                Debug.Log(recipeSO.recipeName);
                waitingRecipeList.Add(recipeSO);
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
                    return;
                }
            }
        }

        Debug.Log("Player did not deliver the right recipe!");
    }
}
