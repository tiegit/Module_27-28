using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private readonly float _spawnTime;
    private readonly IReadOnlyList<DeathConditionPair> _deathConditions;

    public Enemy(IEnumerable<DeathConditionPair> conditions, MonoBehaviour coroutineRunner)
    {
        _spawnTime = Time.time;
        _deathConditions = new List<DeathConditionPair>(conditions);

        coroutineRunner.StartCoroutine(RandomDeathTimer());
    }

    public IReadOnlyList<DeathConditionPair> DeathConditions => _deathConditions;
    public bool IsDead { get; private set; }
    public float SpawnTime => _spawnTime;

    private IEnumerator RandomDeathTimer()
    {
        float randomDelay = Random.Range(2f, 10f);

        yield return new WaitForSeconds(randomDelay);

        IsDead = true;

        Debug.Log("Враг пометил себя как IsDead по случайному таймеру.");
    }

    public IEnumerable<DeathReason> GetActiveDeathReasons()
    {
        foreach (var pair in _deathConditions)
        {
            if (pair.Condition(this))
                yield return pair.Reason;
        }
    }
}
