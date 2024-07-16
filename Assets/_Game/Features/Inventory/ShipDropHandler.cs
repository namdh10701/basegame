using System.Linq;
using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using _Game.Features.MyShip.GridSystem;
using UnityEngine;

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
                    _placementPane = GetComponentInChildren<PlacementPane>().transform as RectTransform;
                }

                return _placementPane;
            }
        }

        public ShipGridHighLighter ShipGridHighLighter;
        private RectTransform _placementPane;

        private void Awake()
        {
            
        }

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
            // rect.anchorMin = Vector2.zero;
            // rect.anchorMax = Vector2.zero;
            rect.anchorMin = Vector2.up;
            rect.anchorMax = Vector2.up;
            rect.pivot = Vector2.zero;
            // rect.anchoredPosition = localAnchoredPosition;
            rect.anchoredPosition = uiCell.anchoredPosition;

            ShipGridHighLighter.Clear();

            shipSetupItem.InventoryItem = inventoryItem;
            shipSetupItem.Removable = true;
            
            ShipGridHighLighter.ActiveCells.ForEach(v =>
            {
                v.Data = inventoryItem;
            });
            
            IOC.Resolve<InventorySheet>().AddIgnore(inventoryItem);

            return true;
        }
    }
}