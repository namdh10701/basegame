using System.Collections.Generic;
using UnityEngine;
namespace _Base.Scripts.UI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField] private List<RectTransform> _parentCells;

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
                _gridConfig.grids[i].cells = new Cell[_gridConfig.grids[i].rows, _gridConfig.grids[i].cols];
                for (int r = 0; r < _gridConfig.grids[i].rows; r++)
                {
                    for (int c = 0; c < _gridConfig.grids[i].cols; c++)
                    {
                        var cell = Instantiate<Cell>(_gridConfig.cell, _parentCells[i]);
                        cell.gameObject.name = $"Cell {r},{c}";
                        var cellData = new CellData(StatusCell.Empty, new Vector2(r, c));
                        cell.Setup(cellData);
                        _gridConfig.grids[i].cells[r, c] = cell;
                        _gridConfig.grids[i].parent = _parentCells[i];
                    }
                }

            }
        }

        private void LoadInventoryItemsOnGrids()
        {
            foreach (var grid in _gridConfig.grids)
            {
                if (grid.inventoryItemsConfig.inventoryItems.Count == 0) continue;

                foreach (var inventoryItem in grid.inventoryItemsConfig.inventoryItems)
                {
                    var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, this.transform);
                    item.Setup(inventoryItem.inventoryItemData);
                    grid.inventoryItems.Add(item);
                }

            }
        }

        private void LoadInventoryItemsOnStash()
        {
            foreach (var grid in _gridConfig.grids)
            {
                if (grid.id == "Stash")
                {
                    foreach (var inventoryItem in grid.inventoryItemsConfig.inventoryItems)
                    {
                        var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, this.transform);
                        item.Setup(inventoryItem.inventoryItemData);
                        grid.inventoryItems.Add(item);
                    }

                    PlaceAllItems(grid.inventoryItems, grid);
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
                            // PlaceItem(item, i, j);
                            item.gameObject.transform.position = GetPositionCell(i, j, grid.cells);
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

        private Vector3 GetPositionCell(int r, int c, Cell[,] cells)
        {
            return new Vector3(cells[r, c].transform.position.x, cells[r, c].transform.position.y, cells[r, c].transform.position.z);
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

        public void PlaceItem(int[,] item, int startX, int startY)
        {
            int itemRows = item.GetLength(0);
            int itemCols = item.GetLength(1);

            for (int i = 0; i < itemRows; i++)
            {
                for (int j = 0; j < itemCols; j++)
                {
                    if (item[i, j] == 1)
                    {
                        // grid[startX + i, startY + j] = (int)StatusCell.Occupied;
                    }
                }
            }
        }



    }

}