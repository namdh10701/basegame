using UnityEngine;
using UnityEngine.EventSystems;

public class PointClickDetectorUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] MenuManager _menuManager;
    [SerializeField] DragItem _prefabDragItem;

    bool _isDown;
    private float timeThreshold = 0.5f;
    private float elapsedTime = 0f;
    private DragItem _dragItem;
    DragItemUI _dragItemUI;

    public void GetComponentMemuManager()
    {
        _menuManager = this.gameObject.GetComponentInParent<MenuManager>();
    }


    private GameObject _itemSelected;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.tag != "ItemMenuSetup")
            return;

        _isDown = true;

        _itemSelected = eventData.pointerEnter;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDown = false;
    }

    void Update()
    {
        if (_isDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeThreshold)
            {
                var itemData = _itemSelected.GetComponentInParent<ItemMenu>();
                if (itemData != null)
                {
                    CreateDragItem(itemData.GetItemMenuData());

                }
                _isDown = false;
                elapsedTime = 0f;
            }
        }
        if (_dragItem != null)
        {
            DragItem();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_dragItem == null)
                return;
            _dragItem.GetCellSelectFromDragItem(_dragItem.GetItemMenuData());
            Destroy(_dragItem.gameObject);
        }
    }

    public void CreateDragItem(ItemMenuData itemMenuData)
    {
        var mousePosition = Input.mousePosition;
        var worldPosition = _menuManager.Camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0; // Ensure z-position is appropriate for 2D space

        // Disable scroll rect to prevent scrolling while dragging
        _menuManager.EnableScrollRect(false);

        // Create drag item UI and set its position
        _dragItemUI = _menuManager.CreateDragItemUI(itemMenuData, worldPosition);

        // Instantiate drag item if it doesn't exist
        if (_dragItem == null)
        {
            _dragItem = Instantiate(_prefabDragItem);
            _dragItem.Setup(itemMenuData);

        }
    }

    public void DragItem()
    {
        var pos = _menuManager.Camera.ScreenToWorldPoint(_dragItemUI.transform.position);
        _dragItem.transform.position = pos;
    }


}
