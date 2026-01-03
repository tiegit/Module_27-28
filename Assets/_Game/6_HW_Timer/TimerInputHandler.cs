using UnityEngine;
using UnityEngine.UI;

public class TimerInputHandler : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _resetButton;

    private Timer _timer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _timer.TimerFinished += OnTimerFinished;

        SetButtonsVisibility(false);
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartClicked);
        _pauseButton.onClick.AddListener(OnPauseClicked);
        _stopButton.onClick.AddListener(OnStopClicked);
        _resetButton.onClick.AddListener(OnResetClicked);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartClicked);
        _pauseButton.onClick.RemoveListener(OnPauseClicked);
        _stopButton.onClick.RemoveListener(OnStopClicked);
        _resetButton.onClick.RemoveListener(OnResetClicked);
    }

    private void OnStartClicked()
    {
        if (_timer.IsPaused)
            _timer.Resume();
        else
            _timer.StartTimer();

        SetButtonsVisibility(true);
    }

    private void OnPauseClicked()
    {
        _timer.Pause();
        SetButtonsVisibility(false);
    }

    private void OnStopClicked()
    {
        _timer.Stop();
        SetButtonsVisibility(false);
    }

    private void OnResetClicked()
    {
        _timer.Reset();
        SetButtonsVisibility(false);
    }

    private void SetButtonsVisibility(bool showPause)
    {
        _startButton.gameObject.SetActive(!showPause);
        _pauseButton.gameObject.SetActive(showPause);
    }
    private void OnTimerFinished() => SetButtonsVisibility(false);

    private void OnDestroy()
    {
        if (_timer != null)
            _timer.TimerFinished -= OnTimerFinished;
    }
}
