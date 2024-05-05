using _Game.Scripts;
using _Game.Scripts.Entities;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsMenu : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    List<GridItemDef> bullets;
    List<GameObject> _menuItem = new List<GameObject>();
    GameObject _menu;
    float distance;
    public void Setup(List<GridItemData> gridItemDatas)
    {
        bullets = new List<GridItemDef>();
        foreach (GridItemData gridItemData in gridItemDatas)
        {
            if (gridItemData.Def.Type == GridItemType.Bullet)
            {
                bullets.Add(gridItemData.Def);
            }
        }
        _menu = new GameObject("Menu");
        _menu.transform.SetParent(this.transform);
        distance = spriteRenderer.bounds.size.x;
        CreateMenu();
    }

    public void CreateMenu()
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
    }
}
