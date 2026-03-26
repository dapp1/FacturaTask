using UnityEngine;

namespace EventBusSystem
{
    public class Events
    {
        public class OnGameEndEvent
        {
            public GameObject Object;

            public OnGameEndEvent(GameObject obj)
            {
                Object = obj;
            }
        }

        public class OnGameStartedEvent
        {

        }

        public class OnGameCompleteEvent
        {

        }

        public class OnCarTakeDamage
        {
            public int Health;

            public OnCarTakeDamage(int health) 
            {
                Health = health;
            }
        }
    }
}
