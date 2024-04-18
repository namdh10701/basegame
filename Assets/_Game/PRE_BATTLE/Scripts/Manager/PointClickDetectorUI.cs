using UnityEngine;
using UnityEngine.EventSystems;

public class PointClickDetectorUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isDown;
    private float timeThreshold = 0.5f;
    private float elapsedTime = 0f;

    private GameObject _itemSelected;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.tag != "ItemMenuSetup")
            return;

        isDown = true;
        _itemSelected = eventData.pointerEnter;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
    }

    void Update()
    {
        if (isDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeThreshold)
            {
                var itemData = _itemSelected.GetComponentInParent<ItemMenu>();
                if (itemData == null)
                {
                    return;
                }
                SetupWeaponsManager.Instance.CreateDragItem(itemData?.GetItemMenuData());
                isDown = false;
                elapsedTime = 0f;
            }
        }
    }


}
