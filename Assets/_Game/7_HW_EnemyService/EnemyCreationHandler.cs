using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreationHandler : MonoBehaviour
{
    public event Action<DeathReason> AddReasonButtonClicked;
    public event Action CreateButtonClicked;

    [SerializeField] private Button _logicalReasonButton;
    [SerializeField] private Button _timeExpiredReasonButton;
    [SerializeField] private Button _countExceededReasonButton;

    [SerializeField] private Button _createEnemyButton;

    private void OnEnable()
    {
        _logicalReasonButton.onClick.AddListener(AddLogicalDeathReason);
        _timeExpiredReasonButton.onClick.AddListener(AddTimeExpiredReason);
        _countExceededReasonButton.onClick.AddListener(AddCountExceededReason);

        _createEnemyButton.onClick.AddListener(CreateEnemyButtonClicked);
    }

    private void OnDisable()
    {
        _logicalReasonButton.onClick.RemoveListener(AddLogicalDeathReason);
        _timeExpiredReasonButton.onClick.RemoveListener(AddTimeExpiredReason);
        _countExceededReasonButton.onClick.RemoveListener(AddCountExceededReason);

        _createEnemyButton.onClick.RemoveListener(CreateEnemyButtonClicked);
    }

    private void AddLogicalDeathReason() => AddReasonButtonClicked?.Invoke(DeathReason.LogicalDeath);
    private void AddTimeExpiredReason() => AddReasonButtonClicked?.Invoke(DeathReason.TimeExpired);
    private void AddCountExceededReason() => AddReasonButtonClicked?.Invoke(DeathReason.CountExceeded);

    private void CreateEnemyButtonClicked() => CreateButtonClicked?.Invoke();
}