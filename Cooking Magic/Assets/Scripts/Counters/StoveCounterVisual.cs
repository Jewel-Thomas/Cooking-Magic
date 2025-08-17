using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    private enum StoveMode
    {
        OnWhileFrying,
        OnWhileBurnt
    }

    [SerializeField] private StoveMode stoveMode;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool isFrying;
        switch (stoveMode)
        {
            case StoveMode.OnWhileFrying:
                isFrying = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
                break;
            case StoveMode.OnWhileBurnt:
                isFrying = e.state != StoveCounter.State.Idle;
                break;
            default:
                isFrying = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
                break;
        }

        HandleVisual(isFrying);
    }

    private void HandleVisual(bool isFrying)
    {
        stoveOnGameObject.SetActive(isFrying);
        particlesGameObject.SetActive(isFrying);
    }
}
