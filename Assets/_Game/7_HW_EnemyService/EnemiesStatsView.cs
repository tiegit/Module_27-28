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

        _service.EnemiesCountChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        var stats = _service.GetStatistics();
        int aliveCount = stats.aliveCount;
        var deathStats = stats.deathStats;

        if (aliveCount == 0)
        {
            _statsText.text = "Врагов нет";
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"<b>Всего врагов: {aliveCount}</b>");
        sb.AppendLine("<size=80%>Причины смерти:</size>");

        foreach (var kvp in deathStats.OrderByDescending(x => x.Value))
            sb.AppendLine($"- {kvp.Key}: {kvp.Value}");

        _statsText.text = sb.ToString();
    }


    private void OnDestroy()
    {
        if (_service != null)
            _service.EnemiesCountChanged -= UpdateUI;
    }
}
