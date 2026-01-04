using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;

public class EnemyStatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _statsText;

    private EnemyService _service;

    public void Initialize(EnemyService service) => _service = service;

    private void OnEnable()
    {
        if (_service != null)
        {
            _service.OnEnemiesChanged += RefreshUI;

            RefreshUI();
        }
    }

    private void OnDisable()
    {
        if (_service != null)
            _service.OnEnemiesChanged -= RefreshUI;
    }

    private void RefreshUI()
    {
        if (_service == null || _statsText == null)
            return;

        var enemies = _service.Enemies;

        if (enemies.Count == 0)
        {
            _statsText.text = "Врагов нет";

            return;
        }
            
        var stats = enemies
            .SelectMany(e => e.DeathConditions)
            .GroupBy(pair => pair.Reason)
            .Select(group => $"{group.Key}: {group.Count()}");

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"<b>Всего врагов: {enemies.Count}</b>");
        sb.AppendLine("<size=80%>Распределение условий:</size>");

        foreach (var line in stats)
            sb.AppendLine($"- {line}");

        _statsText.text = sb.ToString();
    }
}
