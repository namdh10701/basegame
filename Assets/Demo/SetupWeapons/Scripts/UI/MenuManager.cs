
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
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
                var ItemMenu = temp.GetComponent<ItemMenu>();
                _itemMenus.Add(ItemMenu);
                ItemMenu.Setup(item);
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

    public void EnableScrollRect(bool enable)
    {
        _scrollRect.enabled = enable;
    }

    public DragItemUI CreateDragItemUI(ItemMenuData itemMenuData, Vector3 position)
    {
        if (_dragItemUI == null)
        {
            _dragItemUI = Instantiate(_prefabDragItemUI, this.transform);
            _dragItemUI.transform.position = position;

        }
        _dragItemUI.Setup(itemMenuData, _canvas);
        return _dragItemUI;

    }
}
