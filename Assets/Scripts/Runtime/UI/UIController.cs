using EventBusSystem;
using UnityEngine;
using static EventBusSystem.Events;

namespace Assets.Scripts.Runtime.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RectTransform _winScreen;
        [SerializeField] private RectTransform _lostScreen;
        [SerializeField] private RectTransform _tutorScreen;

        [Header("Way")]
        [SerializeField] private WayBar _wayBar;
        [SerializeField] private float _fillDuration = 60f;

        private bool _isGameEnd;

        private void Awake()
        {
            _lostScreen.gameObject.SetActive(false);
            _winScreen.gameObject.SetActive(false);
            _tutorScreen.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!_isGameEnd && _wayBar.Evaluate(_fillDuration))
            {
                _isGameEnd = true;
                EventBus.Publish(new OnGameEndEvent(_winScreen.gameObject));
            }
        }

        public void GameLost()
        {
            EventBus.Publish(new OnGameEndEvent(_lostScreen.gameObject));
        }
    }
}