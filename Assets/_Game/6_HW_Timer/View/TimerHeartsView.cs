using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHeartsView : MonoBehaviour
{
    [SerializeField] private GameObject _heartsContainer;
    [SerializeField] private GameObject _heartPrefab;
    [SerializeField] private int _maxHeartsCount = 10;

    [SerializeField, Space(15)] private AnimationCurve _animationCurve;

    private Timer _timer;
    private List<GameObject> _hearts = new List<GameObject>();
    private int _lastSecond;
    private Coroutine _scalingCoroutine;

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _timer.ElapsedTimeChanged += OnTimeChanged;
        _timer.TimerReset += OnTimerStoppedOrReseted;
        _timer.TimerStopped += OnTimerStoppedOrReseted;
        _timer.TimerFinished += OnTimerStoppedOrReseted;

        _hearts = new List<GameObject>();
        _lastSecond = -1;

        foreach (Transform child in _heartsContainer.transform)
            Destroy(child.gameObject);

        _hearts.Clear();
    }

    private void OnTimeChanged()
    {
        int currentSecond = Mathf.FloorToInt(_timer.ElapsedTime);

        if (currentSecond > _lastSecond && currentSecond < _maxHeartsCount && currentSecond < _timer.InitialTime)
        {
            foreach (var heart in _hearts)
                if (heart != null)
                    heart.SetActive(true);

            GameObject newHeart = Instantiate(_heartPrefab, _heartsContainer.transform);
            newHeart.transform.localScale = Vector3.zero;
            _hearts.Add(newHeart);

            if (_scalingCoroutine != null)
                StopCoroutine(_scalingCoroutine);

            _scalingCoroutine = StartCoroutine(ScaleAllHeartsRoutine());

            _lastSecond = currentSecond;
        }
    }

    private void OnTimerStoppedOrReseted()
    {
        ClearHearts();
        _lastSecond = -1;
    }

    private IEnumerator ScaleAllHeartsRoutine()
    {
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            yield return new WaitWhile(() => _timer.IsPaused);

            float curveValue = _animationCurve.Evaluate(time / duration);

            foreach (var heart in _hearts)
                if (heart != null)
                    heart.transform.localScale = Vector3.one * curveValue;

            time += Time.deltaTime;

            yield return null;
        }

        float finalValue = _animationCurve.Evaluate(1f);

        foreach (var heart in _hearts)
        {
            if (heart != null)
            {
                heart.transform.localScale = Vector3.one * finalValue;

                heart.SetActive(false);
            }
        }

        _scalingCoroutine = null;
    }

    private void ClearHearts()
    {
        if (_scalingCoroutine != null)
        {
            StopCoroutine(_scalingCoroutine);
            _scalingCoroutine = null;
        }

        foreach (var heart in _hearts)
            if (heart != null)
                Destroy(heart);

        _hearts.Clear();
    }

    private void OnDestroy()
    {
        if (_timer != null)
        {
            _timer.ElapsedTimeChanged -= OnTimeChanged;
            _timer.TimerReset -= OnTimerStoppedOrReseted;
            _timer.TimerStopped -= OnTimerStoppedOrReseted;
            _timer.TimerFinished -= OnTimerStoppedOrReseted;
        }

        ClearHearts();
    }
}
