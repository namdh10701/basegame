using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsMenu : MonoBehaviour
{
    /*[SerializeField] SpriteRenderer spriteRenderer;
    List<WeaponItemData> _weaponItemDatas;
    List<GameObject> _menuItem = new List<GameObject>();
    GameObject _menu;
    float distance;
    public void Setup(List<WeaponItemData> weaponItemDatas)
    {
        _menu = new GameObject("Menu");
        _menu.transform.SetParent(this.transform);
        _weaponItemDatas = weaponItemDatas;
        distance = spriteRenderer.bounds.size.x;
        CreateMenu();
    }

    public void CreateMenu()
    {
        for (int i = 0; i < _weaponItemDatas.Count; i++)
        {
            GameObject menu = new GameObject("BulletItem");
            menu.transform.parent = _menu.transform;
            menu.transform.localRotation = Quaternion.Euler(0f, 0f, i * 360 / _weaponItemDatas.Count);
            _menuItem.Add(menu);
        }
        CreateItemMenu();
    }
    public void CreateItemMenu()
    {
        for (int i = 0; i < _menuItem.Count; i++)
        {
            GameObject itemMenu = new GameObject("Icon");
            itemMenu.tag = "BulletsMenu";
            var spr = itemMenu.AddComponent<SpriteRenderer>();
            spr.sprite = _weaponItemDatas[i].itemMenuData.sprite;

            string sortingLayerName = "Default";
            int sortingLayerID = SortingLayer.NameToID(sortingLayerName);
            spr.sortingLayerID = sortingLayerID;

            var temp = itemMenu.AddComponent<BulletItem>();
            temp.Setup(_weaponItemDatas[i].itemMenuData.id, spr);

            var col = itemMenu.AddComponent<BoxCollider2D>();
            col.size = new Vector2(0.35f, 0.35f);
            col.offset = new Vector2(0, 0);


            itemMenu.transform.parent = _menuItem[i].transform;
            itemMenu.transform.localPosition = new Vector3(0, distance / 2, 0);
        }
    }*/
}
