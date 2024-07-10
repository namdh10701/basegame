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
        public Hammer Hammer;
        public ReloadSign Reload;
        bool isFixing;
        bool isReloading;
        public void SetCannon(Cannon cannon)
        {
            this.cannon = cannon;
            CannonStats cannonStats = cannon.Stats as CannonStats;
            AmmoBar.SetProgress(cannonStats.Ammo.Value / cannonStats.Ammo.MaxValue);
            HpBar.SetProgress(cannonStats.HealthPoint.Value / cannonStats.HealthPoint.MaxValue);
            cannonStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
            cannonStats.Ammo.OnValueChanged += Ammo_OnValueChanged;
            cannon.OnFeverStart += Cannon_OnFeverStart;
            cannon.OnFeverEnded += Cannon_OnFeverEnded;
        }

        void Cannon_OnFeverStart()
        {

        }

        void Cannon_OnFeverEnded()
        {

        }

        public void RegisterJob(CrewJobData crewJobData)
        {
            crewJobData.ReloadCannonJobsDic[cannon].StatusChanged += ReloadCannonStatusEnter;
            crewJobData.FixGridItemDic[cannon.GetComponent<IGridItem>()].StatusChanged += FixItemStatusEnter;
        }

        void ReloadCannonStatusEnter(JobStatus status)
        {
            switch (status)
            {
                case JobStatus.Deactive:
                    Reload.Hide();
                    break;
                case JobStatus.Free:
                    Reload.Show();
                    break;
                case JobStatus.WorkingOn:
                    Reload.Play();
                    break;
            }
            isReloading = (status != JobStatus.Deactive);

            if (isFixing)
            {
                Reload.Hide();
            }
        }

        void FixItemStatusEnter(JobStatus status)
        {
            switch (status)
            {
                case JobStatus.Deactive:
                    Hammer.Hide();
                    break;
                case JobStatus.Free:
                    Hammer.Show();
                    if (isReloading)
                    {
                        Reload.Show();
                        Reload.Play();
                    }
                    break;
                case JobStatus.WorkingOn:
                    Hammer.Play();

                    break;
            }
            isFixing = (status != JobStatus.Deactive);
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