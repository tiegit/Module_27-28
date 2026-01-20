using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyService
{
    public event Action EnemiesCountChanged;

    private readonly Dictionary<Enemy, List<(Func<Enemy, bool> condition, string name)>> _enemyConditions = new();
    public IReadOnlyList<Enemy> Enemies => _enemyConditions.Keys.ToList();
    public int EnemiesCount => _enemyConditions.Count;

    public void CustomUpdate()
    {
        var enemiesToKill = new List<Enemy>();

        foreach (var kvp in _enemyConditions)
        {
            var enemy = kvp.Key;
            var conditions = kvp.Value;

            var triggered = conditions
                .Where(c => c.condition(enemy))
                .ToList();

            if (triggered.Count > 0)
            {
                string reason = string.Join(", ", triggered.Select(c => c.name));

                Debug.Log($"Враг уничтожен по условиям: {reason}");

                enemiesToKill.Add(enemy);
            }
        }

        foreach (var enemy in enemiesToKill)
        {
            enemy.Kill();
            _enemyConditions.Remove(enemy);
            EnemiesCountChanged?.Invoke();
        }
    }

    public void RegisterEnemy(Enemy enemy, List<(Func<Enemy, bool> condition, string name)> conditions)
    {
        if (enemy == null)
            throw new ArgumentNullException(nameof(enemy));

        if (conditions == null || conditions.Count == 0)
            throw new ArgumentException("Условия смерти не могут быть null или пустыми.");

        _enemyConditions[enemy] = new List<(Func<Enemy, bool>, string)>(conditions);

        EnemiesCountChanged?.Invoke();
    }

    public (int aliveCount, Dictionary<string, int> deathStats) GetStatistics()
    {
        int aliveCount = _enemyConditions.Count;

        var deathStats = new Dictionary<string, int>();

        foreach (var conditions in _enemyConditions.Values)
        {
            foreach (var condition in conditions)
            {
                if (deathStats.ContainsKey(condition.name))
                    deathStats[condition.name]++;
                else
                    deathStats[condition.name] = 1;
            }
        }

        return (aliveCount, deathStats);
    }


    public void ActivateLogicalDeath(Func<Enemy, bool> conditionToFind)
    {
        var enemyEntry = _enemyConditions.FirstOrDefault(pair =>
            pair.Value.Any(c => c.condition == conditionToFind));

        if (enemyEntry.Key != null)
        {
            Debug.Log($"EnemyService: Логическая смерть активирована для {enemyEntry.Key}");
            enemyEntry.Key.Kill();
        }
        else
        {
            Debug.LogWarning("Враг с логическим условием смерти не найден.");
        }
    }
}
