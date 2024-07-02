using _Base.Scripts.EventSystem;
using _Game.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;

public class BulletsMenu : MonoBehaviour
{
    /* [SerializeField] SpriteRenderer spriteRenderer;
     List<GridItemDef> bullets;
     List<GameObject> _menuItem = new List<GameObject>();
     GameObject _menu;

     float distance;*/

    public BulletButton[] buttons;
    Cannon cannon;
    public CrewJobData jobdata;
    private void Awake()
    {
        GlobalEvent.Register("ReloadCompleted", OnReloadComplete);
    }

    void OnReloadComplete()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void Setup(Cannon cannon, List<Bullet> gridItemDatas)
    {
        jobdata = FindAnyObjectByType<CrewJobData>();
        this.cannon = cannon;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < gridItemDatas.Count; i++)
        {
            Bullet bullet = gridItemDatas[i];
            buttons[i].gameObject.SetActive(true);
            buttons[i].Init(bullet);
            buttons[i].onClick.AddListener(() => Reload(bullet));

        }

        foreach (BulletButton bb in buttons)
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

    void Reload(Bullet bullet)
    {
        Debug.Log("BULLET + " + cannon + " " + bullet + " ");
        GlobalEvent<Cannon, Bullet, int>.Send("Reload", cannon, bullet, int.MaxValue);
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
