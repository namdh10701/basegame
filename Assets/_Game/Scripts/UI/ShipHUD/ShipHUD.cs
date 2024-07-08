using _Base.Scripts.EventSystem;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class ShipHUD : MonoBehaviour
    {
        public AmmoButton[] buttons;
        CrewJobData jobdata;
        Cannon selectingCannon;
        void OnReloadComplete()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public void Setup(Cannon cannon, List<Ammo> gridItemDatas)
        {
            jobdata = FindAnyObjectByType<CrewJobData>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < gridItemDatas.Count; i++)
            {
                Ammo bullet = gridItemDatas[i];
                buttons[i].gameObject.SetActive(true);
                buttons[i].Init(bullet);
                buttons[i].onClick.AddListener(() => Reload(bullet));

            }

            foreach (AmmoButton bb in buttons)
            {
                if (bb.bullet == jobdata.ReloadCannonJobsDic[cannon].bullet)
                {
                    bb.selector.gameObject.SetActive(true);
                }
                else
                {
                    bb.selector.gameObject.SetActive(false);
                }
            }


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