using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "StickmanConfig", menuName = "Configs/Stickman")]
    public class StickmanConfig : ScriptableObject
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}