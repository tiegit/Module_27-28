using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;

public class EnemiesStatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _statsText;

    private EnemyService _service;

    public void Initialize(EnemyService service)
    {
        _service = service;

        _service.OnEnemiesChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
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

    private void OnDestroy()
    {
        if (_service != null)
            _service.OnEnemiesChanged -= UpdateUI;
    }
}
