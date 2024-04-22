using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
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
            if (stat.Value == stat.MinValue)
            {
                OnOutOfAmmo();
            }
            else if (stat.Value == stat.MaxValue)
            {
                OnReloaded();
            }
        }

        public void OnOutOfAmmo()
        {
            cannonRenderer.Blink();
        }
        public void OnReloaded()
        {
            cannonRenderer.StopBlink();
        }
    }
}