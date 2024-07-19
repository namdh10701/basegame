using System;
using System.Collections.Generic;
using System.Linq;
using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using _Game.Features.MyShip.GridSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Features.Inventory
{
    public class ShipDropHandler : DropHandler
    {
        public RectTransform PlacementPane
        {
            get
            {
                if (!_placementPane)
                {
                    _placementPane = GetComponentInChildren<PlacementPane>(false).transform as RectTransform;
                }
                
                return _placementPane;
            }
        }

        public ShipGridHighlighter shipGridHighlighter;
        private RectTransform _placementPane;
        private ShipConfigManager shipConfigManager;

        private void Awake()
        {
            shipConfigManager = GetComponent<ShipConfigManager>();
        }

        // public override ItemDroppedCallbackCommand OnItemDrop(DraggableItem droppedItem)
        // {
        //     var inventoryItem = droppedItem.DragDataProvider.GetData<InventoryItem>();
        //
        //     var cell = shipGridHighlighter.ActiveCells.LastOrDefault();
        //     if (!cell || cell.Data != null) return new ItemDroppedCallbackCommand { Cmd = ItemDroppedCallbackCommand.Command.REJECT };
        //
        //     var beforeProcessData = cell.Data;
        //     
        //     var shipSetupItemPrefab = ShipSetupUtils.GetShipSetupItemPrefab(inventoryItem);
        //     var shipSetupItem = Instantiate(shipSetupItemPrefab, PlacementPane);
        //     shipSetupItem.Positions = shipGridHighlighter.ActiveCells.Select(v => v.Position).ToList();
        //
        //     var uiCell =
        //         GridLayoutGroupUtils.GetCellAtPosition(shipGridHighlighter.Grid.GridLayoutGroup, cell.Position);
        //
        //     var rect = shipSetupItem.transform as RectTransform;
        //     rect.anchorMin = Vector2.up;
        //     rect.anchorMax = Vector2.up;
        //     rect.pivot = Vector2.zero;
        //     rect.anchoredPosition = uiCell.anchoredPosition;
        //
        //     shipGridHighlighter.Clear();
        //
        //     shipSetupItem.InventoryItem = inventoryItem;
        //     shipSetupItem.Removable = true;
        //     
        //     shipGridHighlighter.ActiveCells.ForEach(v =>
        //     {
        //         v.Data = inventoryItem;
        //     });
        //     
        //     IOC.Resolve<InventorySheet>().AddIgnore(inventoryItem);
        //
        //     Dictionary<Vector2Int, InventoryItem> ItemPositions = new();
        //     foreach (var child in _placementPane.GetComponentsInChildren<DraggableItem>())
        //     {
        //         var item = child.DragDataProvider.GetData<InventoryItem>();
        //         
        //         var positions = child.GetComponent<ShipSetupItem>().Positions;
        //         foreach (var pos in positions)
        //         {
        //             ItemPositions[pos] = item;
        //         }
        //     }
        //     
        //     IOC.Resolve<NewShipEditSheet>().ItemPositions = ItemPositions;
        //     shipConfigManager.OnItemPositionChanged.Invoke(shipConfigManager.ItemPositions);
        //     return new ItemDroppedCallbackCommand { Cmd = ItemDroppedCallbackCommand.Command.COMMIT, Data = beforeProcessData };
        // }
        
        
        public override ItemDroppedCallbackCommand OnItemDrop(DraggableItem droppedItem)
        {
            var inventoryItem = droppedItem.DragDataProvider.GetData<InventoryItem>();

            // var cell = shipGridHighlighter.ActiveCells.LastOrDefault();
            var cellPos = Vector2IntArrayUtils.GetTopLeftPoint(shipGridHighlighter.ActiveCells.Select(v => v.Position).ToList());
            var cell = shipGridHighlighter.ActiveCells.First(v => v.Position == cellPos);
            
            if (!cell || cell.Data != null) return new ItemDroppedCallbackCommand { Cmd = ItemDroppedCallbackCommand.Command.REJECT };
            
            
            //
            var beforeProcessData = cell.Data;
            //
            // var shipSetupItemPrefab = ShipSetupUtils.GetShipSetupItemPrefab(inventoryItem);
            // var shipSetupItem = Instantiate(shipSetupItemPrefab, PlacementPane);
            // shipSetupItem.Positions = shipGridHighlighter.ActiveCells.Select(v => v.Position).ToList();
            //
            var uiCell =
                GridLayoutGroupUtils.GetCellAtPosition(shipGridHighlighter.Grid.GridLayoutGroup, cell.Position);
            
            var shipEditSheet = IOC.Resolve<NewShipEditSheet>();
            shipEditSheet.Ship_SetSlot(cell.Position, inventoryItem);
            //
            // var rect = shipSetupItem.transform as RectTransform;
            // rect.anchorMin = Vector2.up;
            // rect.anchorMax = Vector2.up;
            // rect.pivot = Vector2.zero;
            // rect.anchoredPosition = uiCell.anchoredPosition;
            //
            shipGridHighlighter.Clear();
            //
            // shipSetupItem.InventoryItem = inventoryItem;
            // shipSetupItem.Removable = true;
            //
            // shipGridHighlighter.ActiveCells.ForEach(v =>
            // {
            //     v.Data = inventoryItem;
            // });
            //
            // IOC.Resolve<InventorySheet>().AddIgnore(inventoryItem);
            //
            // Dictionary<Vector2Int, InventoryItem> ItemPositions = new();
            // foreach (var child in _placementPane.GetComponentsInChildren<DraggableItem>())
            // {
            //     var item = child.DragDataProvider.GetData<InventoryItem>();
            //     
            //     var positions = child.GetComponent<ShipSetupItem>().Positions;
            //     foreach (var pos in positions)
            //     {
            //         ItemPositions[pos] = item;
            //     }
            // }
            //
            // IOC.Resolve<NewShipEditSheet>().ItemPositions = ItemPositions;
            // shipConfigManager.OnItemPositionChanged.Invoke(shipConfigManager.ItemPositions);
            return new ItemDroppedCallbackCommand { Cmd = ItemDroppedCallbackCommand.Command.COMMIT, Data = beforeProcessData };
        }
    }
}