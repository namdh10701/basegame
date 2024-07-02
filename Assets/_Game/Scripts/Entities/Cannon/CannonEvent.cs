using UnityEngine;

namespace _Game.Scripts.Entities.CannonComponent
{
    public class CannonEvent : MonoBehaviour
    {
        [SerializeField] Cannon cannon;
        [SerializeField] CannonRenderer cannonRenderer;

        private void Start()
        {
            CannonStats cannonStats = (CannonStats)cannon.Stats;
            cannonStats.Ammo.OnValueChanged += Ammo_OnValueChanged;
        }

        private void Ammo_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat stat)
        {
            if (stat.StatValue.BaseValue == stat.MinValue)
            {
                OnOutOfAmmo();
            }
            else if (stat.StatValue.BaseValue == stat.MaxValue)
            {
                OnReloaded();
            }
        }

        public void OnOutOfAmmo()
        {
            cannon.OnOutOfAmmo();
            cannonRenderer.Blink();
        }
        public void OnReloaded()
        {
            cannon.OnReloaded();
            cannonRenderer.StopBlink();
        }



    }
}