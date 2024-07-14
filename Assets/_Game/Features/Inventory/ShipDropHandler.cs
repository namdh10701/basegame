using System.Linq;
using _Game.Features.MyShip.GridSystem;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class ShipDropHandler : DropHandler
    {
        public RectTransform PlacementPane;
        public ShipGridHighLighter ShipGridHighLighter;
        public override bool OnItemDrop(DraggableItem droppedItem)
        {
            var inventoryItem = droppedItem.DragDataProvider.GetData<InventoryItem>();

            var cell = ShipGridHighLighter.ActiveCells.LastOrDefault();
            if (!cell || cell.Data != null) return false;

            var shipSetupItemPrefab = ShipSetupUtils.GetShipSetupItemPrefab(inventoryItem);
            
            var shipSetupItem = Instantiate(shipSetupItemPrefab, PlacementPane);

            var uiCell =
                GridLayoutGroupUtils.GetCellAtPosition(ShipGridHighLighter.Grid.GridLayoutGroup, cell.Position);

            // EditorGUIUtility.PingObject(uiCell);
            
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, uiCell.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(PlacementPane, screenPoint, Camera.main, out Vector2 localAnchoredPosition);
            
            var rect = shipSetupItem.transform as RectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.zero;
            rect.pivot = Vector2.zero;
            rect.anchoredPosition = localAnchoredPosition;

            ShipGridHighLighter.Clear();

            shipSetupItem.InventoryItem = inventoryItem;
            shipSetupItem.Removable = true;
            
            ShipGridHighLighter.ActiveCells.ForEach(v =>
            {
                v.Data = inventoryItem;
            });

            return true;
        }
    }
}