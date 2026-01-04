using System;
using System.Collections.Generic;
using UnityEngine;

public class Example : IDisposable
{
    private EnemyService _service;
    private float _timerDuration;
    private int _maxEnemyCount;

    private EnemyCreationHandler _enemyCreationHandler;

    private List<DeathConditionPair> _selectedDeathPairs = new List<DeathConditionPair>();

    public Example(EnemyService service, EnemyCreationHandler enemyCreationHandler, float timerDuration, int maxEnemyCount)
    {
        _service = service;

        _enemyCreationHandler = enemyCreationHandler;
        _enemyCreationHandler.AddReasonButtonClicked += OnAddReasonButtonClicked;
        _enemyCreationHandler.CreateButtonClicked += OnCreateEnemyButtonClicked;

        _timerDuration = timerDuration;
        _maxEnemyCount = maxEnemyCount;
    }

    private void OnAddReasonButtonClicked(DeathReason reason)
    {
        if (_selectedDeathPairs.Exists(p => p.Reason == reason))
            return;

        Func<Enemy, bool> conditionLogic = reason switch
        {
            DeathReason.LogicalDeath => enemy => enemy.IsDead,
            DeathReason.TimeExpired => enemy => (Time.time - enemy.SpawnTime) > _timerDuration,
            DeathReason.CountExceeded => _ => _service.EnemiesCount > _maxEnemyCount,
            _ => null
        };

        if (conditionLogic != null)
            _selectedDeathPairs.Add(new DeathConditionPair(reason, conditionLogic));
        else
            Debug.LogWarning($"Логика для {reason} не реализована!");
    }

    private void OnCreateEnemyButtonClicked()
    {
        if (_selectedDeathPairs.Count > 0)
        {
            _service.RegisterEnemy(new List<DeathConditionPair>(_selectedDeathPairs));

            Debug.Log($"Враг создан с условиями: {string.Join(", ", _selectedDeathPairs.ConvertAll(p => p.Reason))}");

            _selectedDeathPairs.Clear();
        }
        else
        {
            Debug.LogWarning("Нельзя создать врага без условий смерти!");
        }
    }

    public void Dispose()
    {
        if (_enemyCreationHandler != null)
        {
            _enemyCreationHandler.AddReasonButtonClicked -= OnAddReasonButtonClicked;
            _enemyCreationHandler.CreateButtonClicked -= OnCreateEnemyButtonClicked;
        }
    }
}
