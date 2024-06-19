using _Base.Scripts.EventSystem;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.Gameplay.Ship;
using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletsMenu : MonoBehaviour
{
    /* [SerializeField] SpriteRenderer spriteRenderer;
     List<GridItemDef> bullets;
     List<GameObject> _menuItem = new List<GameObject>();
     GameObject _menu;

     float distance;*/

    public Button[] buttons;
    Cannon cannon;
    public void Setup(Cannon cannon, List<Bullet> gridItemDatas)
    {
        this.cannon = cannon;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < gridItemDatas.Count; i++)
        {
            Bullet bullet = gridItemDatas[i];
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponent<Image>().sprite = gridItemDatas[i].Def.Image;
            buttons[i].onClick.AddListener(() => Reload(bullet));
        }
    }

    void Reload(Bullet bullet)
    {
        GlobalEvent<Cannon, Bullet>.Send("Reload", cannon, bullet);
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

    /* public void CreateMenu()
     {
         for (int i = 0; i < bullets.Count; i++)
         {
             GameObject menu = new GameObject("BulletItem");
             menu.transform.parent = _menu.transform;
             menu.transform.localRotation = Quaternion.Euler(0f, 0f, i * 360 / bullets.Count);
             _menuItem.Add(menu);
         }
         CreateItemMenu();
     }
     public void CreateItemMenu()
     {
         for (int i = 0; i < _menuItem.Count; i++)
         {
             var itemMenu = Instantiate(ResourceLoader.LoadGridItemPrefab(bullets[i]));
             itemMenu.transform.parent = _menuItem[i].transform;
             itemMenu.transform.localPosition = new Vector3(0, distance / 2, 0);
         }
     }*/
}
