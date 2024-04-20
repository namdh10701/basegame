
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuBattleManager : MenuManager
{
    [Header("Config Data")]
    [SerializeField] DataShips _shipConfig;

    [Header("Prefab DragItemUI")]
    [SerializeField] DragItemUI _prefabDragItemUI;
    [SerializeField] ItemMenu _prefabItemMenu;
    [SerializeField] Transform _content;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] Canvas _canvas;

    List<ItemMenu> _itemMenus = new List<ItemMenu>();
    private DragItemUI _dragItemUI;

    void Awake()
    {
        Initialize();
        Application.quitting += QuitGame;
        _scrollRect.verticalNormalizedPosition = 1;
    }

    private void QuitGame()
    {
        // ResetData();
    }

    private void Initialize()
    {
        foreach (var ship in _shipConfig.ships)
        {
            foreach (var weaponItemData in ship.weaponItemDatas)
            {
                switch (weaponItemData.itemMenuData.itemType)
                {
                    case ItemType.Bullet:
                        var item = Instantiate(_prefabItemMenu, _content);
                        item.Setup(weaponItemData.itemMenuData);
                        _itemMenus.Add(item);
                        break;
                }
            }
        }
    }

    public override void EnableScrollRect(bool enable)
    {
        _scrollRect.enabled = enable;
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

    public override void CreateDragItemUI(ItemMenuData itemMenuData, Vector3 position)
    {
        if (_dragItemUI == null)
        {
            _dragItemUI = Instantiate(_prefabDragItemUI, this.transform);
            _dragItemUI.transform.position = position;

        }
        _dragItemUI.Setup(itemMenuData, _canvas);
    }
}
