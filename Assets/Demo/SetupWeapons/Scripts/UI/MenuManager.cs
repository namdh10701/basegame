
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Config Data")]
    [SerializeField] MenuSetupConfig _shipConfig;
    [SerializeField] ItemMenu _prefabItemMenu;
    [SerializeField] Transform _content;

    List<ItemMenu> _itemMenus = new List<ItemMenu>();

    private TabType _curentTab = TabType.Gun;

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

}
