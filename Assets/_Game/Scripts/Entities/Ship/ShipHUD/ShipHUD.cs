using _Base.Scripts.EventSystem;
using _Game.Scripts.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace _Game.Features.Gameplay
{
    public class ShipHUD : MonoBehaviour
    {
        public Cannon Cannon
        {
            get => selectingCannon; set
            {

                if (selectingCannon != null && selectingCannon != value)
                {
                    selectingCannon.border.SetActive(false);
                }
                selectingCannon = value;
            }
        }
        public AmmoButton[] buttons;
        CrewJobData jobdata;
        Cannon selectingCannon;
        public List<Ammo> ammos = new List<Ammo>();


        List<AmmoButton> actives = new List<AmmoButton>();
        public void Initialize(List<Ammo> ammos, CrewJobData jobData)
        {
            this.jobdata = jobData;
            this.ammos = ammos;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < ammos.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].Init(ammos[i]);
                actives.Add(buttons[i]);
            }
        }
        public void OnClick(Ammo ammo)
        {
            GlobalEvent<Cannon, Ammo, int>.Send("Reload", selectingCannon, ammo, int.MaxValue);
            foreach (AmmoButton am in actives)
            {
                if (am.ammo == jobdata.ReloadCannonJobsDic[selectingCannon].bullet)
                {
                    am.ToggleSelect(true);
                }
                else
                {
                    am.ToggleSelect(false);
                }
            }
        }

        public void Show()
        {
            selectingCannon.border.SetActive(true);
            gameObject.SetActive(true);
            foreach (AmmoButton am in actives)
            {
                if (am.ammo == jobdata.ReloadCannonJobsDic[selectingCannon].bullet)
                {
                    am.ToggleSelect(true);
                }
                else
                {
                    am.ToggleSelect(false);
                }
            }
        }

        public void Hide()
        {
            selectingCannon?.border.SetActive(false);
            gameObject.SetActive(false);
        }


        void Reload(Ammo bullet)
        {
            GlobalEvent<Cannon, Ammo, int>.Send("Reload", selectingCannon, bullet, int.MaxValue);
            Close();
        }

        void Close()
        {
            transform.parent.gameObject.SetActive(false);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
}