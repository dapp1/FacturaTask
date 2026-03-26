using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;
using static EventBusSystem.Events;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillBar;

    private void Start()
    {
        EventBus.Subscribe<OnCarTakeDamage>(UpdateFill);
    }

    private void UpdateFill(OnCarTakeDamage ev)
    {
        _fillBar.fillAmount = (float)ev.Health / 100;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<OnCarTakeDamage>(UpdateFill);
    }
}
