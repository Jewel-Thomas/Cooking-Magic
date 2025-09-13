using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManagerUISingle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private Transform ingredientTemplate;

    private void Awake()
    {
        ingredientTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeText.text = recipeSO.recipeName;

        foreach (Transform child in ingredientContainer)
        {
            if (child == ingredientTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjectsSO in recipeSO.ingredientList)
        {
            Transform ingredientTransform = Instantiate(ingredientTemplate, ingredientContainer);
            ingredientTransform.gameObject.SetActive(true);
            ingredientTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }
}
