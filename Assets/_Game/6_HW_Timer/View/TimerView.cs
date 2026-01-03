using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Toggle _inverseTimeToggle;
    [SerializeField] private TMP_Text timerText;

    private Timer _timer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _timer.ElapsedTimeChanged += OnElapsedTimeChanged;
        _inverseTimeToggle.onValueChanged.AddListener(OnToggleValueChanged);

        OnElapsedTimeChanged();
    }

    private void OnElapsedTimeChanged()
    {
        if (_timer == null || timerText == null)
            return;

        string timeText;

        if (_inverseTimeToggle.isOn)
        {
            float elapsedTime = _timer.ElapsedTime;
            timeText = FormatTime(elapsedTime);
        }
        else
        {
            float remainingTime = Mathf.Max(0, _timer.InitialTime - _timer.ElapsedTime);
            timeText = FormatRemainingTime(remainingTime);
        }

        timerText.text = timeText;
    }

    private void OnToggleValueChanged(bool isOn) => OnElapsedTimeChanged();

    private string FormatTime(float seconds)
    {
        int hours = (int)(seconds / 3600);
        int minutes = (int)((seconds / 60) % 60);
        int secs = (int)(seconds % 60);
        return $"{hours:D2}:{minutes:D2}:{secs:D2}";
    }

    private string FormatRemainingTime(float seconds)
    {
        int totalSeconds = Mathf.CeilToInt(seconds);
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int secs = totalSeconds % 60;
        return $"{hours:D2}:{minutes:D2}:{secs:D2}";
    }

    private void OnDestroy()
    {
        if (_timer != null)
            _timer.ElapsedTimeChanged -= OnElapsedTimeChanged;

        if (_inverseTimeToggle != null)
            _inverseTimeToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }
}
