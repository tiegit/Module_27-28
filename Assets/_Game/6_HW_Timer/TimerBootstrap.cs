using UnityEngine;

public class TimerBootstrap : MonoBehaviour
{
    [SerializeField] private float _timerTime = 10f;
    [SerializeField] private TimerInputHandler _inputHandler;

    [SerializeField, Space(15)] private TimerHeartsView _timerHeartsView;

    [SerializeField, Space(15)] private TimerBarView _timerBarView;

    [SerializeField, Space(15)] private TimerView _timerView;

    private void Awake()
    {
        Timer timer = new Timer(this);

        timer.SetTimerValue(_timerTime);

        _inputHandler.Initialize(timer);

        _timerHeartsView.Initialize(timer);

        _timerBarView.Initialize(timer);

        _timerView.Initialize(timer);
    }
}
