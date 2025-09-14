using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public static event EventHandler OnAnyItemDropped;
    public virtual void Interact(Player player)
    {
        Debug.LogError("Illegal BaseCounter Interact() called!");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("Illegal BaseCounter InteractAlternate() called!");
    }

    public Transform GetKitchenOjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyItemDropped?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
