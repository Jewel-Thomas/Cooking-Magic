using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpBindingText;
    [SerializeField] private TextMeshProUGUI keyMoveDownBindingText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftBindingText;
    [SerializeField] private TextMeshProUGUI keyMoveRightBindingText;
    [SerializeField] private TextMeshProUGUI keyInteractBindingText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateBindingText;
    [SerializeField] private TextMeshProUGUI keyPauseBindingText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractBindingText;
    [SerializeField] private TextMeshProUGUI keyGamepadInteractAlternateBindingText;
    [SerializeField] private TextMeshProUGUI keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnRebindingComplete += GameInput_OnRebindingComplete;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Show();
        UpdateVisual();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.isCountDownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnRebindingComplete(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternateBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPauseBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyGamepadInteractBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        keyGamepadInteractAlternateBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
