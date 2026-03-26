using Player;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "Configs/Turret")]
    public class TurretConfig : ScriptableObject
    {
        [field : SerializeField] public float ReloadTime { get; private set; }
        [field : SerializeField] public float Sensativity { get; private set; }
        [field : SerializeField] public Projectile ProjectilePrefab { get; private set; }
    }
}