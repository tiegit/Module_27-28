using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private readonly float _spawnTime;
    private readonly IReadOnlyList<DeathCondition> _deathConditions;

    public Enemy(IEnumerable<DeathCondition> conditions, MonoBehaviour coroutineRunner)
    {
        _spawnTime = Time.time;

        var conditionsList = new List<DeathCondition>(conditions);
        _deathConditions = conditionsList;

        if (conditionsList.Exists(enemy => enemy.Reason == DeathReason.LogicalDeath))
            coroutineRunner.StartCoroutine(RandomDeathTimer());
    }

    public IReadOnlyList<DeathCondition> DeathConditions => _deathConditions;
    public bool IsDead { get; private set; }
    public float SpawnTime => _spawnTime;

    public IEnumerable<DeathReason> GetActiveDeathReasons()
    {
        foreach (var pair in _deathConditions)
        {
            if (pair.Condition(this))
                yield return pair.Reason;
        }
    }

    private IEnumerator RandomDeathTimer()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) == true);

        IsDead = true;

        Debug.Log("Враг пометил себя как IsDead по клику игрока.");
    }
}
