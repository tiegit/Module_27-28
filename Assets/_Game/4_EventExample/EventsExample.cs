using System;
using UnityEngine;

public class EventsExample : MonoBehaviour
{
    public event Action<int> HealthChanged;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HealthChanged?.Invoke(10);
        }
    }
}
