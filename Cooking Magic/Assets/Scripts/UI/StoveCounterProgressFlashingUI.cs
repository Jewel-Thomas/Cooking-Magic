using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterProgressFlashingUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        SetFlashingBar(false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float showWarnProgress = 0.5f;
        bool show = stoveCounter.isFried() && e.progressNormalized > showWarnProgress;
        SetFlashingBar(show);
    }

    private void SetFlashingBar(bool isFlashing)
    {
        animator.SetBool(IS_FLASHING, isFlashing);
    }
}
