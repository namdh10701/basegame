using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] Image _backGroup;
    [SerializeField] Image _icon;
    private ItemMenuData _itemMenuData;
    private Color _oldColor;

    public void Setup(ItemMenuData itemMenuData)
    {
        _itemMenuData = itemMenuData;
        _icon.sprite = itemMenuData.sprite;
        _oldColor = _backGroup.color;
        
        if (itemMenuData.isSelected)
            _backGroup.color = Color.grey;


        _icon.SetNativeSize();
    }
    public ItemMenuData GetItemMenuData()
    {
        return _itemMenuData;
    }

    public void EnableItemMenu(bool enable)
    {
        if (!enable)
            _backGroup.color = Color.grey;
        else
            _backGroup.color = _oldColor;


        this.gameObject.GetComponent<PointClickDetectorUI>().enabled = enable;
        _itemMenuData.isSelected = !enable;

    }

}
