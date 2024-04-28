
using _Game.Scripts;
using _Game.Scripts.Entities;
using ExitGames.Client.Photon.StructWrapping;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MenuManager : MonoBehaviour
{
    public Camera Camera;
    //public abstract void EnableScrollRect(bool enable);
    //public abstract DragItemUI CreateDragItemUI(ItemMenuData itemMenuData, Vector3 position);
}

public class MenuPreBattle : MonoBehaviour
{

    [Header("Prefab DragItemUI")]
    [SerializeField] DragItemUI _prefabDragItemUI;
    [SerializeField] ItemMenu _prefabItemMenu;
    [SerializeField] Transform _content;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] Canvas _canvas;
    [SerializeField] DragController DragController;

    public GridItemReferenceHolder GridItemReferenceHolder;

    List<ItemMenu> _itemMenus = new List<ItemMenu>();

    private GridItemType _curentTab = GridItemType.Cannon;
    private GridItem draggingItem;

    List<GridItemReferenceWrapper> cannonReferences = new List<GridItemReferenceWrapper>();
    List<GridItemReferenceWrapper> bulletReferences = new List<GridItemReferenceWrapper>();
    List<GridItemReferenceWrapper> crewReferences = new List<GridItemReferenceWrapper>();
    void Awake()
    {
        foreach (GridItemReference gir in GridItemReferenceHolder.CannonReferences)
        {
            cannonReferences.Add(new GridItemReferenceWrapper(gir));
        }
        foreach (GridItemReference gir in GridItemReferenceHolder.BulletReferences)
        {
            bulletReferences.Add(new GridItemReferenceWrapper(gir));
        }
        foreach (GridItemReference gir in GridItemReferenceHolder.CrewReferences)
        {
            crewReferences.Add(new GridItemReferenceWrapper(gir));
        }
        DragController.OnGridItemPlaced += OnGridItemPlaced;
        DragController.OnGridItemUp += OnGridItemUp;
        Initialize();
    }

    private void OnGridItemPlaced(GridItemReference reference)
    {
        for (int i = 0; i < cannonReferences.Count; i++)
        {
            if (cannonReferences[i].gridItemReference.Equals(reference))
            {
                Debug.Log("POPOPOP");
                cannonReferences[i].gridItemReference.Selected = true;
            }
        }
        for (int i = 0; i < bulletReferences.Count; i++)
        {
            if (bulletReferences[i].gridItemReference.Equals(reference))
            {
                bulletReferences[i].gridItemReference.Selected = true;
            }
        }
        for (int i = 0; i < crewReferences.Count; i++)
        {
            if (crewReferences[i].gridItemReference.Equals(reference))
            {
                crewReferences[i].gridItemReference.Selected = true;
            }
        }
        foreach (ItemMenu itemMenu in _itemMenus)
        {
            if (itemMenu.GridItemReference.Equals(reference))
            {
                itemMenu.GridItemReference.Selected = true;
                itemMenu.Disable();
            }
        }
    }
    private void OnGridItemUp(GridItemReference reference)
    {
        for (int i = 0; i < cannonReferences.Count; i++)
        {
            if (cannonReferences[i].gridItemReference.Equals(reference))
            {
                Debug.Log("OPOPOP");
                cannonReferences[i].gridItemReference.Selected = false;
            }
        }
        for (int i = 0; i < bulletReferences.Count; i++)
        {
            if (bulletReferences[i].gridItemReference.Equals(reference))
            {
                bulletReferences[i].gridItemReference.Selected = false;
            }
        }
        for (int i = 0; i < crewReferences.Count; i++)
        {
            if (crewReferences[i].gridItemReference.Equals(reference))
            {
                crewReferences[i].gridItemReference.Selected = false;
            }
        }
        foreach (ItemMenu itemMenu in _itemMenus)
        {
            if (itemMenu.GridItemReference.Equals(reference))
            {
                itemMenu.GridItemReference.Selected = false;
                itemMenu.Enable();
            }
        }
    }
    private void OnEnable()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }

    public void SwitchTab(int tabType)
    {
        _curentTab = (GridItemType)tabType;
        Initialize();
    }

    private void Initialize()
    {
        if (_prefabItemMenu == null)
        {
            return;
        }
        if (_itemMenus.Count > 0)
        {
            RemoveItemMenus();
        }
        List<GridItemReferenceWrapper> itemList = null;
        switch (_curentTab)
        {
            case GridItemType.Cannon:
                itemList = cannonReferences;
                break;
            case GridItemType.Bullet:
                itemList = bulletReferences;
                break;
            case GridItemType.Crew:
                itemList = crewReferences;
                break;
        }

        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                var temp = Instantiate(_prefabItemMenu, _content);
                temp.Setup(item.gridItemReference, DragController.OnPointerDown, DragController.OnPointerUp, item.gridItemReference.Selected);

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
