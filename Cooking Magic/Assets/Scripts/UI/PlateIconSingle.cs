using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingle : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    public void SetPlateIcon(KitchenObjectsSO kitchenObjectsSO)
    {
        iconImage.sprite = kitchenObjectsSO.sprite;
    }
}
