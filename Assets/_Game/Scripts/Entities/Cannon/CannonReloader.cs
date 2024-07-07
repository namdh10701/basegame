using _Base.Scripts.RPG.Behaviours.AttackTarget;
using UnityEngine;
namespace _Game.Scripts.Entities.CannonComponent
{
    public class CannonReloader : MonoBehaviour
    {
        [SerializeField] Cannon cannon;
        [SerializeField] AttackTargetBehaviour AttackTargetBehaviour;

        public void Reload(Ammo bullet)
        {
            cannon.usingBullet = bullet;
            CannonStats cannonStats = (CannonStats)cannon.Stats;
            AmmoStats ps = (AmmoStats)bullet.Stats;
            cannonStats.Ammo.MaxStatValue.BaseValue = ps.MagazineSize.Value;
            cannonStats.Ammo.MinStatValue.BaseValue = 0;
            cannonStats.Ammo.StatValue.BaseValue = ps.MagazineSize.Value;
            AttackTargetBehaviour.projectilePrefab = bullet.Projectile;
        }
    }
}
