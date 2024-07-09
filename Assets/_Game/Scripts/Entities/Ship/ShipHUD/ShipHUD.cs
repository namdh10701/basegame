using _Base.Scripts.EventSystem;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ShipHUD : MonoBehaviour
    {
        public Cannon Cannon { get => selectingCannon; set => selectingCannon = value; }
        public AmmoButton[] buttons;
        CrewJobData jobdata;
        Cannon selectingCannon;
        public List<Ammo> ammos = new List<Ammo>();


        List<AmmoButton> actives = new List<AmmoButton>();
        public void Initialize(List<Ammo> ammos)
        {
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
        public void Show()
        {
            gameObject.SetActive(true);
            foreach (AmmoButton am in actives)
            {
                if (am.ammo == selectingCannon.usingBullet)
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