using UnityEngine;
using TMPro;
using System;

public class GameStartCountdownTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStartCountDownTimerText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.isCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        gameStartCountDownTimerText.text = Mathf.Ceil(GameManager.Instance.GetGameCountdownStartTimer()).ToString();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
