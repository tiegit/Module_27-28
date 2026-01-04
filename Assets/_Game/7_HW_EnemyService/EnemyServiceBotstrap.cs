using UnityEngine;

public class EnemyServiceBotstrap : MonoBehaviour
{
    [SerializeField] private EnemyStatsView _enemyStatsView;

    [SerializeField] private EnemyCreationHandler _enemyCreationHandler;

    [SerializeField] private float _timerDuration = 10f;
    [SerializeField] private int _maxEnemyCount = 5;

    private EnemyService _enemyService;
    private Example _example;

    private void Awake()
    {
        _enemyService = new EnemyService(this);

        _example = new Example(_enemyService, _enemyCreationHandler, _timerDuration, _maxEnemyCount);

        _enemyStatsView.Initialize(_enemyService);
    }

    private void Update()
    {
        if (_enemyService != null)
            _enemyService.Update();
    }

    private void OnDestroy()
    {
        if (_example != null)
            _example.Dispose();
    }
}