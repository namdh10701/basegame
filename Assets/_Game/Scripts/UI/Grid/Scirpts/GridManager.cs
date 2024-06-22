using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField] private List<RectTransform> _parentCells;
        public List<InventoryItem> inventoryItems = new List<InventoryItem>();


        void Awake()
        {
            Initialize();
            // LoadInventoryItemsOnGrids();
            LoadInventoryItemsOnStash();
        }

        private void Initialize()
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                grid.cells = new Cell[grid.rows, grid.cols];

                for (int r = 0; r < grid.rows; r++)
                {
                    for (int c = 0; c < grid.cols; c++)
                    {
                        var cell = Instantiate<Cell>(_gridConfig.cell, _parentCells[i]);
                        cell.gameObject.name = $"Cell {r},{c}";

                        // Tính toán vị trí cell với khoảng cách spacing
                        float posX = c * (grid.cellSize.x + grid.spacing);
                        float posY = r * (grid.cellSize.y + grid.spacing);
                        cell.transform.localPosition = new Vector2(posX, posY);

                        var cellData = new CellData(StatusCell.Empty, new Vector2(r, c), grid.cellSize);
                        cell.Setup(cellData);
                        grid.cells[r, c] = cell;
                    }
                }
            }
        }

        private void LoadInventoryItemsOnGrids()
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                if (grid.inventoryItemsConfig.inventoryItems.Count == 0) continue;

                foreach (var inventoryItem in grid.inventoryItemsConfig.inventoryItems)
                {
                    var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, _parentCells[i]);
                    item.Setup(inventoryItem.inventoryItemData);
                    // grid.inventoryItems.Add(item);
                }

            }
        }

        private void LoadInventoryItemsOnStash()
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                if (grid.id == "Stash")
                {
                    foreach (var inventoryItem in grid.inventoryItemsConfig.inventoryItems)
                    {
                        var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, _parentCells[i]);
                        inventoryItem.inventoryItemData.gridID = grid.id;
                        item.Setup(inventoryItem.inventoryItemData);
                        inventoryItems.Add(item);
                    }

                    PlaceAllItems(inventoryItems, grid);
                }
            }

        }

        private void PlaceAllItems(List<InventoryItem> inventoryItems, GridInfor grid)
        {
            foreach (var item in inventoryItems)
            {
                bool itemPlaced = false;

                for (int i = 0; i < grid.rows && !itemPlaced; i++)
                {
                    for (int j = 0; j < grid.cols && !itemPlaced; j++)
                    {
                        if (CanPlaceItem(item.GetShape(), i, j, grid))
                        {
                            PlaceItem(item, i, j, GetPositionCell(i, j, grid.cells), grid);
                            itemPlaced = true;
                        }
                    }
                }

                if (!itemPlaced)
                {
                    Debug.Log("Không thể đặt item vào grid.");
                }
            }
        }

        private GameObject GetPositionCell(int r, int c, Cell[,] cells)
        {
            return cells[r, c].gameObject;
        }

        private bool CanPlaceItem(int[,] shape, int startX, int startY, GridInfor grid)
        {
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);

            // Kiểm tra xem item có vượt quá giới hạn của grid không
            if (startX + itemRows > grid.rows || startY + itemCols > grid.cols)
            {
                return false;
            }

            // Kiểm tra xem vị trí đặt item có bị trùng lặp với các cell đã được đặt không
            for (int i = 0; i < itemRows; i++)
            {
                for (int j = 0; j < itemCols; j++)
                {
                    if (shape[i, j] == 1 && !grid.cells[startX + i, startY + j].CheckCellEmty())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void PlaceItem(InventoryItem inventoryItem, int startX, int startY, GameObject cell, GridInfor grid)
        {
            int itemRows = inventoryItem.GetShape().GetLength(0);
            int itemCols = inventoryItem.GetShape().GetLength(1);

            for (int i = 0; i < itemRows; i++)
            {
                for (int j = 0; j < itemCols; j++)
                {
                    if (inventoryItem.GetShape()[i, j] == 1)
                    {
                        grid.cells[startX + i, startY + j].SetStatusCell(StatusCell.Occupied);
                    }
                }
            }
            Debug.Log("PlaceItem: " + cell.transform.localPosition);

            inventoryItem.gameObject.transform.localPosition = cell.transform.localPosition;

        }


    }

}