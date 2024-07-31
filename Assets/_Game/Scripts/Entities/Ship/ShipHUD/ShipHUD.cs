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
        public AmmoButton[] buttons;
        public List<Ammo> ammos = new List<Ammo>();
        public RectTransform rectTransform;

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
            rectTransform.sizeDelta = new Vector2(2.4f * ammos.Count, 1.1362f);
        }
        public void FilterCannonUsingAmmo(Cannon cannon)
        {
            foreach (AmmoButton am in actives)
            {
                if (am.ammo == cannon.UsingAmmo)
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
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}