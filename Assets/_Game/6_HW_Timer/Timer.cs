using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action TimerFinished;
    public event Action ElapsedTimeChanged;
    public event Action TimerReset;
    public event Action TimerStopped;

    private readonly MonoBehaviour _coroutineRunner;

    private float _initialTime;
    private float _elapsedTime;
    private bool _isPaused;

    private Coroutine timerCoroutine;

    public Timer(MonoBehaviour coroutineRunner) => _coroutineRunner = coroutineRunner;

    public float InitialTime => _initialTime;
    public float ElapsedTime => _elapsedTime;
    public bool IsPaused => _isPaused;
    public bool IsRunning => timerCoroutine != null && !_isPaused;

    public void SetTimerValue(float seconds)
    {
        _initialTime = seconds;
        UpdateTimeLeft(0f);

        _isPaused = false;
    }

    public void StartTimer()
    {
        _isPaused = false;

        if (timerCoroutine != null)
            _coroutineRunner.StopCoroutine(timerCoroutine);

        if (_elapsedTime >= _initialTime)
            UpdateTimeLeft(0f);

        timerCoroutine = _coroutineRunner.StartCoroutine(TimerCoroutine());
    }

    public void Pause() => _isPaused = true;

    public void Resume() => _isPaused = false;

    public void Stop()
    {
        if (timerCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        _isPaused = false;

        UpdateTimeLeft(0f);

        TimerStopped?.Invoke();
    }

    public void Reset()
    {
        Stop();

        StartTimer();

        TimerReset?.Invoke();
    }

    private IEnumerator TimerCoroutine()
    {
        while (_elapsedTime < _initialTime)
        {
            if (!_isPaused)
                UpdateTimeLeft(_elapsedTime + Time.deltaTime);

            yield return null;
        }

        UpdateTimeLeft(_initialTime);

        TimerFinished?.Invoke();
    }

    private void UpdateTimeLeft(float newValue)
    {
        float clampedValue = Mathf.Min(_initialTime, Mathf.Max(0f, newValue));

        if (_elapsedTime != clampedValue)
        {
            _elapsedTime = clampedValue;
            ElapsedTimeChanged?.Invoke();
        }
    }
}
