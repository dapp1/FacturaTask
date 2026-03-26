using EventBusSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using static EventBusSystem.Events;

public class TutorialScreen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _screen;
    [SerializeField] private RectTransform _gameScreen;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _screen.gameObject.SetActive(false);
        _gameScreen.gameObject.SetActive(true);
        EventBus.Publish(new OnGameStartedEvent());
    }
}
