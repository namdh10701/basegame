using _Base.Scripts.RPG.Behaviours.AttackTarget;
using UnityEngine;
namespace _Game.Scripts.Entities.CannonComponent
{
    public class CannonReloader : MonoBehaviour
    {
        [SerializeField] Cannon cannon;
        [SerializeField] AttackTargetBehaviour AttackTargetBehaviour;

        public void Reload(_Base.Scripts.RPGCommon.Entities.Projectile projectile)
        {
            CannonStats cannonStats = (CannonStats)cannon.Stats;
            cannonStats.Ammo.StatValue.BaseValue = cannonStats.Ammo.MaxValue;
            AttackTargetBehaviour.projectilePrefab = projectile;
        }
    }
}
