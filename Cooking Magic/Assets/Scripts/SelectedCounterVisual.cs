using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisuals;
    
    private void Start()
    {
        Player.Instance.OnSelectCounterChanged += Player_OnSelectCounterChanged;
    }

    private void Player_OnSelectCounterChanged(object sender, Player.OnSelectCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject selectedVisual in selectedVisuals)
        {
            selectedVisual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject selectedVisual in selectedVisuals)
        {
            selectedVisual.SetActive(false);
        }
    }
}
