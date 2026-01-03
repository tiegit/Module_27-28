using UnityEngine;using UnityEngine.UI;

public class TimerBarView : MonoBehaviour
{
    [SerializeField] private Image _fillerImage;

    private Timer _timer;

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _timer.ElapsedTimeChanged += OnTimeChanged;

        UpdateFill();
    }

    private void OnTimeChanged() => UpdateFill();

    private void UpdateFill() => _fillerImage.fillAmount = Mathf.Clamp01(_timer.ElapsedTime / _timer.InitialTime);

    private void OnDestroy()
    {
        if (_timer != null)
            _timer.ElapsedTimeChanged -= OnTimeChanged;
    }
}
