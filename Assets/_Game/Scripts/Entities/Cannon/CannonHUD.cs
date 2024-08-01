using _Base.Scripts.RPG.Stats;
using _Base.Scripts.UI;
using _Game.Scripts;
using _Game.Scripts.Entities;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Features.Gameplay
{
    public class CannonHUD : MonoBehaviour, ICannonVisualElement
    {
        Cannon cannon;
        public ProgressBarDisplay AmmoBar;
        public ProgressBarDisplay HpBar;
        public Hammer Hammer;
        public ReloadSign Reload;
        bool isFixing;
        bool isReloading;

        public Canvas canvas;
        public int offsetSort;
        public int offset => offsetSort;

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

        }

        public void RegisterJob(CrewJobData crewJobData)
        {
            //crewJobData.ReloadCannonJobsDic[cannon].StatusChanged += ReloadCannonStatusEnter;
            crewJobData.FixCannonJobDic[cannon].OnStatusChanged += FixItemStatusEnter;
        }


        void FixItemStatusEnter(TaskStatus status)
        {
            isFixing = (status != TaskStatus.Disabled);
            switch (status)
            {
                case TaskStatus.Disabled:
                    Hammer.Hide();
                    if (isReloading)
                    {
                        Reload.Show();
                        Reload.Play();
                    }
                    break;
                case TaskStatus.Pending:
                    Hammer.Show();
                    if (isReloading)
                    {
                        Reload.Hide();
                    }
                    break;
                case TaskStatus.Working:
                    if (isReloading)
                    {
                        Reload.Hide();
                    }
                    Hammer.Play();
                    break;
            }
        }



        private void Ammo_OnValueChanged(RangedStat obj)
        {
            AmmoBar.SetProgress(obj.Value / obj.MaxValue);
            if (obj.Value == 0)
            {
                isReloading = true;
                if (!isFixing)
                {
                    Reload.Show();
                    Reload.Play();
                }
            }
            if (obj.Value > 1)
            {
                isReloading = false;
                Reload.Hide();
            }
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
                amount = Mathf.Clamp01(amount);
                HpBar.gameObject.SetActive(true);
                HpBar.SetProgress((float)amount);
            }
        }

        public void UpdateSorting(Renderer mainRenderer)
        {
            canvas.sortingOrder = mainRenderer.sortingOrder + offset;
        }
    }
}