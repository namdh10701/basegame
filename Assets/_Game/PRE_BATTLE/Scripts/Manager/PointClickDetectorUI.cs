using UnityEngine;
using UnityEngine.EventSystems;

public class PointClickDetectorUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] MenuManager _menuManager;
    [SerializeField] DragItem _prefabDragItem;

    bool isDown;
    private float timeThreshold = 0.5f;
    private float elapsedTime = 0f;
    private DragItem _dragItem;

    public void GetComponentMemuManager()
    {
        _menuManager = this.gameObject.GetComponentInParent<MenuManager>();
    }


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
                if (itemData != null)
                {
                    CreateDragItem(itemData.GetItemMenuData());

                }
                isDown = false;
                elapsedTime = 0f;
            }
        }
    }

    public void CreateDragItem(ItemMenuData itemMenuData)
    {
        var worldPosition = _menuManager.Camera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0; // Ensure z-position is appropriate for 2D space

        // Disable scroll rect to prevent scrolling while dragging
        _menuManager.EnableScrollRect(false);

        // Create drag item UI and set its position
        _menuManager.CreateDragItemUI(itemMenuData, Input.mousePosition);

        // Instantiate drag item if it doesn't exist
        if (_dragItem == null)
        {
            _dragItem = Instantiate(_prefabDragItem, this.transform);
        }

        // Setup drag item and set its position
        _dragItem.Setup(itemMenuData);
        _dragItem.transform.position = worldPosition;
    }


}
