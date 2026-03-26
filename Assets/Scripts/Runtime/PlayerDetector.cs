using System;
using UnityEngine;

namespace Assets.Scripts.Runtime
{
    public class PlayerDetector : MonoBehaviour
    {
        public event Action<GameObject> OnTriggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnTriggerEntered?.Invoke(other.gameObject);
            }
        }
    }
}