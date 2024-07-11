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

            float amount = cannonStats.HealthPoint.Value / cannonStats.HealthPoint.MaxValue;
            if (amount == 1)
            {
                HpBar.gameObject.SetActive(false);
                HpBar.SetProgress((float)amount);
            }
            else
            {
                HpBar.gameObject.SetActive(true);
                HpBar.SetProgress((float)amount);
            }
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

/*        public void RegisterJob(CrewJobData crewJobData)
        {
            //crewJobData.ReloadCannonJobsDic[cannon].StatusChanged += ReloadCannonStatusEnter;
            crewJobData.FixGridItemDic[cannon.GetComponent<IGridItem>()].StatusChanged += FixItemStatusEnter;
        }*/

        void ReloadCannonStatusEnter(JobStatus status)
        {
            isReloading = (status != JobStatus.Deactive);
            Debug.Log("status " + status);
            if (isFixing)
            {
                return;
            }
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
        }

        void FixItemStatusEnter(JobStatus status)
        {
            isFixing = (status != JobStatus.Deactive);
            switch (status)
            {
                case JobStatus.Deactive:
                    Hammer.Hide();
                    break;
                case JobStatus.Free:
                    Hammer.Show();
                    if (isReloading)
                    {
                        Reload.Hide();
                    }
                    break;
                case JobStatus.WorkingOn:
                    Hammer.Play();
                    break;
            }

        }



        private void Ammo_OnValueChanged(RangedStat obj)
        {
            AmmoBar.SetProgress(obj.Value / obj.MaxValue);
        }

        private void HealthPoint_OnValueChanged(RangedStat obj)
        {
            float amount = obj.Value / obj.MaxValue;
            if (amount == 1)
            {
                HpBar.gameObject.SetActive(false);

                HpBar.SetProgress((float)amount);
            }
            else
            {
                HpBar.gameObject.SetActive(true);
                HpBar.SetProgress((float)amount);
            }
        }
    }
}