using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyService
{
    public event Action OnEnemiesChanged;

    private List<Enemy> _enemies = new List<Enemy>();
    private MonoBehaviour _coroutineRunner;

    public EnemyService(MonoBehaviour coroutineRunner) => _coroutineRunner = coroutineRunner;

    public IReadOnlyList<Enemy> Enemies => _enemies;
    public int EnemiesCount => _enemies.Count;

    public void RegisterEnemy(List<DeathCondition> conditions)
    {
        Enemy newEnemy = new Enemy(conditions, _coroutineRunner);
        _enemies.Add(newEnemy);

        OnEnemiesChanged?.Invoke();
    }

    public void CustomUpdate()
    {
        int removedCount = _enemies.RemoveAll(enemy =>
        {
            var reasons = enemy.GetActiveDeathReasons().ToList();

            if (reasons.Count > 0)
            {
                Debug.Log($"Враг уничтожен: {string.Join(", ", reasons)}");

                return true;
            }

            return false;
        });

        if (removedCount > 0)
        {
            Debug.Log($"Зарегистрировано врагов: {_enemies.Count}");
            OnEnemiesChanged?.Invoke();
        }
    }
}
