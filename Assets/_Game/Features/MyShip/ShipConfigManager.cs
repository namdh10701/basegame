using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Features.Inventory;
using _Game.Features.Inventory.Core;
using _Game.Features.MyShip.GridSystem;
using Fusion.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using InventoryItem = _Game.Features.Inventory.InventoryItem;

namespace _Game.Features.MyShip
{
    public class ItemPositionChangedEvent : UnityEvent<Dictionary<Vector2Int, InventoryItem>>
    {
    }

    public class ShipConfigManager : MonoBehaviour
    {
        public ItemPositionChangedEvent OnItemPositionChanged = new();

        [SerializeField] private Dictionary<Vector2Int, InventoryItem> _itemPositions = new();
        public Dictionary<Vector2Int, InventoryItem> ItemPositions
        {
            get => _itemPositions;
            set
            {
                _itemPositions = value;
                // SceneRefDrawer();
            }
        }
        
        RectTransform PlacementPane => GetComponentInChildren<PlacementPane>().transform as RectTransform;
        ShipGridHighlighter shipGridHighlighter => GetComponentInChildren<ShipGridHighlighter>();
        GridLayoutGroup GridLayoutGroup => GetComponentInChildren<SlotGrid>().GetComponent<GridLayoutGroup>();

        public void PlaceInventoryItem(InventoryItem item, Vector2Int pos)
        {
            var shipSetupItemPrefab = ShipSetupUtils.GetShipSetupItemPrefab(item);
            var shipSetupItem = Instantiate(shipSetupItemPrefab, PlacementPane);
            shipSetupItem.InventoryItem = item;
            shipSetupItem.Removable = true;
            shipSetupItem.Positions = shipGridHighlighter.ActiveCells.Select(v => v.Position).ToList();
            
            // cell.Position
            var uiCell = GridLayoutGroupUtils.GetCellAtPosition(GridLayoutGroup, pos);
            
            var rect = shipSetupItem.transform as RectTransform;
            // rect.anchorMin = Vector2.zero;
            // rect.anchorMax = Vector2.zero;
            rect.anchorMin = Vector2.up;
            rect.anchorMax = Vector2.up;
            rect.pivot = Vector2.zero;
            // rect.anchoredPosition = localAnchoredPosition;
            rect.anchoredPosition = uiCell.anchoredPosition;
        }

        public void ClearPlacement()
        {
            
            foreach(Transform child in PlacementPane.transform)
            {
                Destroy(child.gameObject);
            }
        }


        // private InventoryManager manager;

        // private void Awake()
        // {
        //     GridLayoutGroupUtils.GetGridLayoutSize(GridLayoutGroup, out var colCount, out var rowCount);
        //     var slotCells = GridLayoutGroup.GetComponentsInChildren<SlotCell>();
        //     foreach (var slot in slotCells)
        //     {
        //         
        //     }
        //     manager = new InventoryManager(new Vector2Int());
        // }
    }
}
