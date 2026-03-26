using EventBusSystem;
using UnityEngine;
using static EventBusSystem.Events;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        EventBus.Subscribe<OnGameStartedEvent>(OnGameStarted);
        EventBus.Subscribe<OnGameEndEvent>(OnGameEnd);
    }

    private void OnGameStarted(OnGameStartedEvent ev)
    {
        Pause(false);
    }

    private void OnGameEnd(OnGameEndEvent ev)
    {
        ev.Object.SetActive(true);
        Pause(true);
    }

    private void Pause(bool pause)
    {
        int value  = pause ? 0 : 1;
        Time.timeScale = value;
    }
}
