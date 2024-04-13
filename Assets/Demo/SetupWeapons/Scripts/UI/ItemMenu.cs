using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] Image _backGroup;
    [SerializeField] Image _icon;
    private ItemMenuData _itemMenuData;

    public void Setup(ItemMenuData itemMenuData)
    {
        _itemMenuData = itemMenuData;
    }
    public ItemMenuData GetItemMenuData()
    {
        return _itemMenuData;
    }

}
