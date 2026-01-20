using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreationHandler : MonoBehaviour
{
    public event Action AddLogicalDeathReasonClicked;
    public event Action AddTimeExpiredReasonClicked;
    public event Action AddCountExceededReasonClicked;
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

    private void AddLogicalDeathReason() => AddLogicalDeathReasonClicked?.Invoke();
    private void AddTimeExpiredReason() => AddTimeExpiredReasonClicked?.Invoke();
    private void AddCountExceededReason() => AddCountExceededReasonClicked?.Invoke();

    private void CreateEnemyButtonClicked() => CreateButtonClicked?.Invoke();
}