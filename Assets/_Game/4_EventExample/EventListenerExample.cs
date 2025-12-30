using UnityEngine;

public class EventListenerExample : MonoBehaviour
{
    [SerializeField] private EventsExample _example;

    private void OnEnable() => _example.HealthChanged += OnHealthChanged;

    private void OnDisable() => _example.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged(int a)
    {
        Debug.Log($"Сработал + {a}");
        gameObject.SetActive( false );
    }
}