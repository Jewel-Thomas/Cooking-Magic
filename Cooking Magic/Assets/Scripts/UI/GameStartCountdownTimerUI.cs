using UnityEngine;
using TMPro;
using System;

public class GameStartCountdownTimerUI : MonoBehaviour
{
    private const string COUNT_DOWN_ANIMATION_TRIGGER = "NumberPopup";

    [SerializeField] private TextMeshProUGUI gameStartCountDownTimerText;
    private Animator countDownAnimator;

    private int previousCountDownNumber;

    private void Awake()
    {
        countDownAnimator = GetComponent<Animator>();
    }

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
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetGameCountdownStartTimer());
        gameStartCountDownTimerText.text = countDownNumber.ToString();

        if(previousCountDownNumber != countDownNumber)
        {
            previousCountDownNumber = countDownNumber;
            countDownAnimator.SetTrigger(COUNT_DOWN_ANIMATION_TRIGGER);
            SoundManager.Instance.PlayCountDownSound();
        }

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
