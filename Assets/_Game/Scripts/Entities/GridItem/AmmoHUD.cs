using _Base.Scripts.RPG.Stats;
using _Base.Scripts.UI;
using _Game.Features.Gameplay;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class AmmoHUD : MonoBehaviour
    {
        Ammo ammo;
        public ProgressBarDisplay HPBar;
        public Hammer Hammer;
        public void SetAmmo(Ammo ammo)
        {
            this.ammo = ammo;
            AmmoStats ammoStats = ammo.Stats as AmmoStats;
            float amount = ammoStats.HealthPoint.Value / ammoStats.HealthPoint.MaxValue;
            if (amount == 1)
            {
                HPBar.SetProgress((float)amount);
                HPBar.gameObject.SetActive(false);
            }
            else
            {
                HPBar.gameObject.SetActive(true);
                HPBar.SetProgress((float)amount);
            }
            ammoStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        }

        private void HealthPoint_OnValueChanged(RangedStat stat)
        {
            float amount = stat.Value / stat.MaxValue;
            if (amount == 1)
            {
                HPBar.SetProgress((float)amount);
                HPBar.gameObject.SetActive(false);
            }
            else
            {
                HPBar.gameObject.SetActive(true);
                HPBar.SetProgress((float)amount);
            }
        }

/*        public void RegisterJob(CrewJobData crewJobData)
        {
            crewJobData.FixGridItemDic[ammo.GetComponent<IGridItem>()].StatusChanged += EnterState;
        }*/

        public void EnterState(JobStatus jobStatus)
        {
            switch (jobStatus)
            {
                case JobStatus.Deactive:
                    Hammer.Hide();
                    Hammer.Stop();
                    break;
                case JobStatus.Free:
                    Hammer.Show();
                    break;
                case JobStatus.WorkingOn:
                    Hammer.Play();
                    break;
            }
        }

    }
}