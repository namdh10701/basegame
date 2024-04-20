
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MenuManager : MonoBehaviour
{
    public Camera Camera;
    public abstract void EnableScrollRect(bool enable);
    public abstract void CreateDragItemUI(ItemMenuData itemMenuData, Vector3 position);
}

public class MenuPreBattle : MenuManager
{
    [Header("Config Data")]
    [SerializeField] MenuSetupConfig _shipConfig;

    [Header("Prefab DragItemUI")]
    [SerializeField] DragItemUI _prefabDragItemUI;
    [SerializeField] ItemMenu _prefabItemMenu;
    [SerializeField] Transform _content;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] Canvas _canvas;

    List<ItemMenu> _itemMenus = new List<ItemMenu>();

    private TabType _curentTab = TabType.Gun;
    private DragItemUI _dragItemUI;
    void Awake()
    {
        Initialize();
        Application.quitting += QuitGame;
        _scrollRect.verticalNormalizedPosition = 1;
    }

    private void QuitGame()
    {
        ResetData();
    }

    private void ResetData()
    {
        foreach (var collection in new[] { _shipConfig.itemGunDatas, _shipConfig.itemBulletDatas, _shipConfig.itemCharaterDatas, _shipConfig.itemSkinShipDatas })
        {
            foreach (var item in collection)
            {
                item.isSelected = false;
            }
        }
    }

    public void SwitchTab(int tabType)
    {
        _curentTab = (TabType)tabType;
        Initialize();
    }

    private void Initialize()
    {
        if (_itemMenus.Count > 0)
        {
            RemoveItemMenus();
        }
        List<ItemMenuData> itemList = null;
        switch (_curentTab)
        {
            case TabType.Gun:
                itemList = _shipConfig.itemGunDatas;
                break;
            case TabType.Bullet:
                itemList = _shipConfig.itemBulletDatas;
                break;
            case TabType.Character:
                itemList = _shipConfig.itemCharaterDatas;
                break;
            case TabType.SkinShip:
                itemList = _shipConfig.itemSkinShipDatas;
                break;
        }

        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                var temp = Instantiate(_prefabItemMenu, _content);
                temp.Setup(item);
                _itemMenus.Add(temp);
            }
        }
    }

    private void RemoveItemMenus()
    {
        foreach (var item in _itemMenus)
        {
            Destroy(item.gameObject);
        }
        _itemMenus.Clear();
    }

    public override void EnableScrollRect(bool enable)
    {
        _scrollRect.enabled = enable;
    }

    public override void CreateDragItemUI(ItemMenuData itemMenuData, Vector3 position)
    {
        if (_dragItemUI == null)
        {
            _dragItemUI = Instantiate(_prefabDragItemUI, this.transform);
            _dragItemUI.transform.position = position;

        }
        _dragItemUI.Setup(itemMenuData, _canvas);

    }

    public void EnableDragItem(ItemMenuData itemMenuData, bool enable)
    {
        foreach (var item in _itemMenus)
        {
            if (item.GetItemMenuData().id == itemMenuData.id)
            {
                item.EnableItemMenu(enable);
            }
        }
    }

}
