using System;
using UnityEngine;

public class PlatesIconsUI : MonoBehaviour
{
    [SerializeField] private Transform iconsTemplate;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Awake()
    {
        iconsTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateIconsVisual();
    }

    private void UpdateIconsVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconsTemplate) continue;
            Destroy(child.gameObject);   
        }

        foreach (KitchenObjectsSO kitchenObjectsSO in plateKitchenObject.GetIngredientList())
        {
            Transform iconsTemplateTransform = Instantiate(iconsTemplate, transform);
            iconsTemplateTransform.gameObject.SetActive(true);
            iconsTemplateTransform.GetComponent<PlateIconSingle>().SetPlateIcon(kitchenObjectsSO);
        }
    }
}
