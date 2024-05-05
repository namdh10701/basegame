
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


public class MenuPreBattle : MonoBehaviour
{

    [Header("Prefab DragItemUI")]
    [SerializeField] ItemMenu _prefabItemMenu;
    [SerializeField] Transform _content;
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] Canvas _canvas;
    [SerializeField] DragController DragController;


    List<ItemMenu> _itemMenus = new List<ItemMenu>();

    private GridItemType _curentTab = GridItemType.Cannon;
    List<GridItemDef> selectedItems = new List<GridItemDef>();
    List<GridItemDef> inavailableItems = new List<GridItemDef>();
    [SerializeReference] GridItemReferenceHolder GridItemReferenceHolder;
    void Awake()
    {
        DragController.OnGridItemPlaced += OnGridItemPlaced;
        DragController.OnGridItemUp += OnGridItemUp;
        foreach (GridItemData gridItemData in ShipSetup.GridItemDatas)
        {
            selectedItems.Add(gridItemData.Def);
        }
        Initialize();
    }

    private void OnGridItemPlaced(GridItemDef reference)
    {
        foreach (ItemMenu itemMenu in _itemMenus)
        {
            if (itemMenu.GridItemDef.Equals(reference))
            {
                itemMenu.Disable();
            }
        }
        if (!selectedItems.Contains(reference))
        {
            selectedItems.Add(reference);
        }
    }
    private void OnGridItemUp(GridItemDef reference)
    {

        foreach (ItemMenu itemMenu in _itemMenus)
        {
            if (itemMenu.GridItemDef.Equals(reference))
            {
                itemMenu.Enable();
            }
        }
        if (selectedItems.Contains(reference))
        {
            selectedItems.Remove(reference);
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
        GridItemDef[] itemList = null;
        switch (_curentTab)
        {
            case GridItemType.Cannon:
                itemList = GridItemReferenceHolder.CannonReferences;
                break;
            case GridItemType.Bullet:
                itemList = GridItemReferenceHolder.BulletReferences;
                break;
            case GridItemType.Crew:
                itemList = GridItemReferenceHolder.CrewReferences;
                break;
        }

        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                var temp = Instantiate(_prefabItemMenu, _content);
                temp.Setup(item, DragController.OnPointerDown, DragController.OnPointerUp, selectedItems.Contains(item));

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
}
