using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example : IDisposable
{
    private EnemyService _service;
    private EnemyCreationHandler _enemyCreationHandler;
    private float _timerDuration;
    private int _maxEnemyCount;

    private List<(Func<Enemy, bool> condition, string name)> _selectedConditions = new();
    public Example(EnemyService service, EnemyCreationHandler enemyCreationHandler, float timerDuration, int maxEnemyCount, MonoBehaviour coroutineRunner)
    {
        _service = service;

        _enemyCreationHandler = enemyCreationHandler;
        _enemyCreationHandler.AddLogicalDeathReasonClicked += OnAddLogicalDeathReasonClicked;
        _enemyCreationHandler.AddTimeExpiredReasonClicked += OnAddTimeExpiredReasonClicked;
        _enemyCreationHandler.AddCountExceededReasonClicked += OnAddCountExceededReasonClicked;
        _enemyCreationHandler.CreateButtonClicked += OnCreateEnemyButtonClicked;

        _timerDuration = timerDuration;
        _maxEnemyCount = maxEnemyCount;

        coroutineRunner.StartCoroutine(LogicalDeathActivationCoroutine());
    }

    private void OnAddLogicalDeathReasonClicked() => AddReason((enemy => enemy.IsDead, "Логическое уничтожение"));

    private void OnAddTimeExpiredReasonClicked()
    {
        float spawnTime = Time.time;

        AddReason((enemy => Time.time - spawnTime > _timerDuration, $"Время жизни ({_timerDuration}s)"));
    }

    private void OnAddCountExceededReasonClicked() => AddReason((enemy => _service.EnemiesCount > _maxEnemyCount, $"Превышен лимит ({_maxEnemyCount} врагов)"));

    private void AddReason((Func<Enemy, bool> condition, string name) reason)
    {
        if (_selectedConditions.Any(c => c.condition == reason.condition && c.name == reason.name))
        {
            Debug.Log($"Условие уже добавлено: {reason.name}");

            return;
        }

        _selectedConditions.Add(reason);

        Debug.Log($"Добавлено условие: {reason.name}");
    }

    private void OnCreateEnemyButtonClicked()
    {
        if (_selectedConditions.Count == 0)
        {
            Debug.LogWarning("Нет условий смерти — враг не создан.");
            return;
        }

        var enemy = new Enemy();

        _service.RegisterEnemy(enemy, _selectedConditions);

        Debug.Log($"Враг создан с условиями: {string.Join(", ", _selectedConditions.Select(c => c.name))}");

        _selectedConditions.Clear();
    }

    private IEnumerator LogicalDeathActivationCoroutine()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) == true);

        _service.ActivateLogicalDeath(enemy => enemy.IsDead);
    }

    public void Dispose()
    {
        if (_enemyCreationHandler != null)
        {
            _enemyCreationHandler.AddLogicalDeathReasonClicked -= OnAddLogicalDeathReasonClicked;
            _enemyCreationHandler.AddTimeExpiredReasonClicked -= OnAddTimeExpiredReasonClicked;
            _enemyCreationHandler.AddCountExceededReasonClicked -= OnAddCountExceededReasonClicked;
            _enemyCreationHandler.CreateButtonClicked -= OnCreateEnemyButtonClicked;
        }
    }
}
