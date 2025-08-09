using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObject;
    [SerializeField] private Transform counterTopPoint;
    public void Interact()
    {
        Debug.Log("interact!");
        Transform tomatoTransform = Instantiate(kitchenObject.prefab, counterTopPoint);
        tomatoTransform.localPosition = Vector3.zero;
    } 
}
