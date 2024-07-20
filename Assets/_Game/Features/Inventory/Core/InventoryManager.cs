namespace _Game.Features.Inventory.Core
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryManager
    {

        private Slot[] gridData;
        public Vector2Int gridSize;

        public InventoryManager(Vector2Int gridSize)
        {
            this.gridSize = gridSize;
            gridData = new Slot[gridSize.x * gridSize.y];
            InitializeGrid();
        }

        public InventoryManager(Vector2Int gridSize, List<Vector2Int> disabledSlots, List<Vector2Int> hiddenSlots)
        {
            this.gridSize = gridSize;
            gridData = new Slot[gridSize.x * gridSize.y];
            InitializeGrid();
            SetDisabledSlots(disabledSlots);
            SetHiddenSlots(hiddenSlots);
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < gridData.Length; i++)
            {
                gridData[i] = new Slot();
            }
        }

        public bool CanPlaceItem(Vector2Int startPosition, InventoryItem item, out List<Vector2Int> placableCells)
        {
            placableCells = new List<Vector2Int>();

            foreach (var shape in item.Shape)
            {
                int row = startPosition.x + shape.x;
                int col = startPosition.y + shape.y;
                int index = row * gridSize.y + col;

                // Check if within bounds
                if (row < 0 || row >= gridSize.x || col < 0 || col >= gridSize.y)
                    return false;

                // Check if the slot is usable and empty
                Slot slot = gridData[index];
                if (slot.IsDisabled || slot.Value != null)
                    return false;

                placableCells.Add(new Vector2Int(row, col));
            }

            return true;
        }

        public void PlaceItem(Vector2Int startPosition, InventoryItem item)
        {
            foreach (var shape in item.Shape)
            {
                int row = startPosition.x + shape.x;
                int col = startPosition.y + shape.y;
                int index = row * gridSize.y + col;
                gridData[index].Value = item;
            }
        }

        public void RemoveItem(string itemId)
        {
            for (int i = 0; i < gridData.Length; i++)
            {
                if (gridData[i].Value is InventoryItem value && value.Id == itemId)
                {
                    gridData[i].Value = null; // Reset to empty slot
                }
            }
        }

        public Slot GetSlot(Vector2Int position)
        {
            int index = position.x * gridSize.y + position.y;
            return gridData[index];
        }
        //
        // public void SetSlot(Vector2Int position, Slot slot)
        // {
        //     int index = position.x * gridSize.y + position.y;
        //     gridData[index] = slot;
        // }
        //
        // public void SetSlots(List<Vector2Int> positions, Slot slot)
        // {
        //     foreach (var position in positions)
        //     {
        //         SetSlot(position, slot);
        //     }
        // }

        public void SetDisabledSlots(List<Vector2Int> positions)
        {
            foreach (var position in positions)
            {
                gridData[position.x * gridSize.y + position.y].IsDisabled = true;
            }
        }

        public void SetHiddenSlots(List<Vector2Int> positions)
        {
            foreach (var position in positions)
            {
                gridData[position.x * gridSize.y + position.y].IsHidden = true;
            }
        }

        public void PrintGrid()
        {
            for (int r = 0; r < gridSize.x; r++)
            {
                string row = "";
                for (int c = 0; c < gridSize.y; c++)
                {
                    var slot = gridData[r * gridSize.y + c];
                    string value = slot.IsDisabled ? "D" : slot.IsHidden ? "H" : slot.Value?.Id ?? "0";
                    row += value + " ";
                }

                Debug.Log(row);
            }
        }

        public bool MoveItem(Vector2Int currentPos, Vector2Int newPos)
        {
            Slot currentSlot = GetSlot(currentPos);
            if (currentSlot.Value == null || string.IsNullOrEmpty(currentSlot.Value.Id))
                return false; // No valid item at the current position

            var item = currentSlot.Value;

            // Check if the item can be placed at the new position
            if (!CanPlaceItem(newPos, item, out List<Vector2Int> placableCells))
                return false;

            // Remove the item from the current position
            RemoveItem(item.Id);

            // Place the item at the new position
            PlaceItem(newPos, item);

            return true;
        }

        public void RemoveItemAt(Vector2Int position)
        {
            int index = position.x * gridSize.y + position.y;
            if (index < 0 || index >= gridData.Length) return; // Check if the index is out of bounds

            Slot slot = gridData[index];
            if (slot.Value == null || string.IsNullOrEmpty(slot.Value.Id))
                return; // Check if there is an item at the given position

            // Remove the item with the given ID
            RemoveItem(slot.Value.Id);
        }
        
        public void RemoveItemAt(Slot cell)
        {
            if (cell == null || cell.Value == null || string.IsNullOrEmpty(cell.Value.Id)) return; // Check if the cell or item is null

            // Remove the item with the given ID
            RemoveItem(cell.Value.Id);
        }


        public List<Vector2Int> GetItemCells(string itemId)
        {
            List<Vector2Int> occupiedCells = new List<Vector2Int>();

            for (int i = 0; i < gridData.Length; i++)
            {
                if (gridData[i].Value is InventoryItem value && value.Id == itemId)
                {
                    occupiedCells.Add(new Vector2Int(i / gridSize.y, i % gridSize.y));
                }
            }

            return occupiedCells;
        }
    }
}