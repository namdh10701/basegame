using _Base.Scripts.EventSystem;
using _Base.Scripts.RPG.Behaviours.AttackTarget;
using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CannonController : MonoBehaviour
    {
        Cannon cannon;

        public void Init(Cannon cannon)
        {
            this.cannon = cannon;
            CannonStats cannonStats = cannon.Stats as CannonStats;
            cannonStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            cannonStats.Ammo.OnValueChanged += Ammo_OnValueChanged;
        }

        private void Ammo_OnValueChanged(RangedStat stat)
        {
            if (stat.StatValue.BaseValue == stat.MinValue)
            {
                
            }
            else if (stat.StatValue.BaseValue == stat.MaxValue)
            {
                
            }
        }

        private void HealthPoint_OnValueChanged(RangedStat stat)
        {
            if (stat.StatValue.BaseValue == stat.MinValue)
            {
                
            }
            else if (stat.StatValue.BaseValue == stat.MaxValue)
            {
                
            }
        }

        public void TakeHit(float dmg)
        {

            CannonStats cannonStats = cannon.Stats as CannonStats;


            if (cannonStats.HealthPoint.Value <= cannonStats.HealthPoint.MinValue)
            {
                Deactivate();
                GlobalEvent<IGridItem, int>.Send("Fix", cannon, 20);
            }
        }

        public void Deactivate()
        {
            cannon.FindTargetBehaviour.Disable();
        }

        public void Acivate()
        {
            cannon.FindTargetBehaviour.Enable();
        }

        public void Reload()
        {

        }
        public void Reload(Ammo bullet)
        {
            cannon.usingBullet = bullet;
            CannonStats cannonStats = (CannonStats)cannon.Stats;
            AmmoStats ps = (AmmoStats)bullet.Stats;
            cannonStats.Ammo.MaxStatValue.BaseValue = ps.MagazineSize.Value;
            cannonStats.Ammo.MinStatValue.BaseValue = 0;
            cannonStats.Ammo.StatValue.BaseValue = ps.MagazineSize.Value;
            cannon.AttackTargetBehaviour.projectilePrefab = bullet.Projectile;
        }
    }
}