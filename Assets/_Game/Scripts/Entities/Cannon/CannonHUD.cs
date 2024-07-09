using _Base.Scripts.RPG.Stats;
using _Base.Scripts.UI;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Features.Gameplay
{
    public class CannonHUD : MonoBehaviour
    {
        Cannon cannon;
        public ProgressBarDisplay AmmoBar;
        public ProgressBarDisplay HpBar;

        public void Init(Cannon cannon)
        {
            this.cannon = cannon;
            CrewJobData crewJobData = FindAnyObjectByType<CrewJobData>();
            CannonStats cannonStats = cannon.Stats as CannonStats;
            AmmoBar.SetProgress(cannonStats.HealthPoint.Value / cannonStats.HealthPoint.MaxValue);
            HpBar.SetProgress(cannonStats.HealthPoint.Value / cannonStats.HealthPoint.MaxValue);
            cannonStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            cannonStats.Ammo.OnValueChanged += Ammo_OnValueChanged;

        }

        private void Ammo_OnValueChanged(RangedStat obj)
        {
            AmmoBar.SetProgress(obj.Value / obj.MaxValue);
        }

        private void HealthPoint_OnValueChanged(RangedStat obj)
        {
            HpBar.SetProgress(obj.Value / obj.MaxValue);
        }
    }
}