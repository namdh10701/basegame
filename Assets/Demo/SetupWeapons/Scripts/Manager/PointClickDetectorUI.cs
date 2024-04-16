using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointClickDetectorUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isDown;
    private float timeThreshold = 1.0f;
    private float elapsedTime = 0f;

    private GameObject _itemSelected;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag != "ItemMenuSetup")
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
                SetupWeaponsManager.Instance.CreateDragItem(itemData.GetItemMenuData());
                isDown = false;
                elapsedTime = 0f;
            }
        }
    }


}
