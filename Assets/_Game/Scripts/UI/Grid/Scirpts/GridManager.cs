using System.Collections.Generic;
using _Game.Scripts;
using UnityEngine;
namespace _Base.Scripts.UI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField] private List<RectTransform> _parentCells;

        [SerializeField] private InventoryItemsConfig _TestinventoryItemsConfig;
        public List<InventoryItem> inventoryItems = new List<InventoryItem>();

        private List<Cell> _gridStash = new List<Cell>();


        void Awake()
        {
            Initialize();
            // LoadInventoryItemsOnGrids();
            // LoadInventoryItemsOnStash();
            GetInventoryItemInfo(_TestinventoryItemsConfig.inventoryItemsInfo);
        }

        private void Initialize()
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                grid.cells = new Cell[grid.rows, grid.cols];
                if (grid.listCellsData.Count > 0)
                {
                    for (int r = 0; r < grid.rows; r++)
                    {
                        for (int c = 0; c < grid.cols; c++)
                        {
                            var cell = Instantiate<Cell>(_gridConfig.cell, _parentCells[i]);
                            cell.gameObject.name = $"Cell {r},{c}";

                            // Tính toán vị trí cell với khoảng cách spacing
                            float posX = c * (grid.cellSize.x + grid.spacing) + grid.cellSize.x / 2;
                            float posY = r * (grid.cellSize.y + grid.spacing);
                            cell.transform.localPosition = new Vector2(posX, posY);
                            grid.cells[r, c] = cell;
                            var cellData = new CellData(r, c, StatusCell.Empty, new Vector2(posX, posY), grid.cellSize);
                            cell.Setup(cellData);
                            _gridStash.Add(cell);
                        }
                    }
                }
                else
                {
                    for (int r = 0; r < grid.rows; r++)
                    {
                        for (int c = 0; c < grid.cols; c++)
                        {
                            var cell = Instantiate<Cell>(_gridConfig.cell, _parentCells[i]);
                            cell.gameObject.name = $"Cell {r},{c}";

                            // Tính toán vị trí cell với khoảng cách spacing
                            float posX = c * (grid.cellSize.x + grid.spacing) + grid.cellSize.x / 2;
                            float posY = r * (grid.cellSize.y + grid.spacing);
                            cell.transform.localPosition = new Vector2(posX, posY);
                            grid.cells[r, c] = cell;
                            var cellData = new CellData(r, c, StatusCell.Empty, new Vector2(posX, posY), grid.cellSize);
                            grid.listCellsData.Add(cellData);
                            cell.Setup(cellData);
                            _gridStash.Add(cell);
                        }
                    }
                }

                LoadCellData(grid);
            }

        }

        private void LoadCellData(GridInfor grid)
        {
            if (grid.listCellsData.Count == 0) return;

            for (int i = 0; i < grid.listCellsData.Count; i++)
            {
                _gridStash[i].Setup(grid.listCellsData[i]);
                grid.cells[grid.listCellsData[i].r, grid.listCellsData[i].c].Setup(grid.listCellsData[i]);
            }

        }

        // private void LoadInventoryItemsOnGrids()
        // {
        //     for (int i = 0; i < _gridConfig.grids.Count; i++)
        //     {
        //         var grid = _gridConfig.grids[i];
        //         if (grid.inventoryItemsConfig.inventoryItemsInfo.Count == 0) continue;

        //         foreach (var inventoryItem in grid.inventoryItemsConfig.inventoryItemsInfo)
        //         {
        //             var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, _parentCells[i]);
        //             item.Setup(inventoryItem.inventoryItemData);
        //             // grid.inventoryItems.Add(item);
        //         }

        //     }
        // }

        private void GetInventoryItemInfo(List<InventoryItemInfo> inventoryItems)
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                if (grid.id == "Stash")
                {
                    foreach (var inventoryItem in inventoryItems)
                    {
                        if (CheckPlaceItem(inventoryItem, grid))
                        {
                            grid.inventoryItemsConfig.inventoryItemsInfo.Add(inventoryItem);
                        }
                        else
                        {
                            Debug.Log("Can't insert item inventory");
                        }
                        continue;
                    }

                }
                for (int r = 0; r < grid.rows; r++)
                {
                    for (int c = 0; c < grid.cols; c++)
                    {
                        grid.cells[r, c].ChangeColor();
                    }
                }
            }


        }

        [ContextMenu("LoadInventoryItemsOnStash")]
        private void LoadInventoryItemsOnStash()
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                if (grid.id == "Stash")
                {
                    foreach (var inventoryItem in grid.inventoryItemsConfig.inventoryItemsInfo)
                    {
                        var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, _parentCells[i]);
                        inventoryItem.inventoryItemData.gridID = grid.id;
                        item.Setup(inventoryItem.inventoryItemData);
                        inventoryItems.Add(item);

                    }
                }
            }
        }

        public bool CheckPlaceItem(InventoryItemInfo inventoryItemInfo, GridInfor grid)
        {
            bool itemPlaced = false;

            for (int i = 0; i < grid.rows && !itemPlaced; i++)
            {
                for (int j = 0; j < grid.cols && !itemPlaced; j++)
                {
                    var shape = Shape.ShapeDic[inventoryItemInfo.inventoryItemData.shapeId];
                    if (CanPlace(shape, i, j, grid))
                    {
                        var pos = GetPositionCell(shape, i, j, grid.cells, grid);
                        inventoryItemInfo.inventoryItemData.position = pos;
                        ChangeStatusCell(inventoryItemInfo.inventoryItemData, i, j, pos, grid);
                        itemPlaced = true;
                    }
                }
            }

            if (!itemPlaced)
            {
                return false;
            }

            return true;
        }

        private Vector2 GetPositionCell(int[,] shape, int r, int c, Cell[,] cells, GridInfor grid)
        {
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);
            var rect = new Vector2(cells[r, c].gameObject.transform.localPosition.x + grid.cellSize.x / 2 * (itemCols - 1), cells[r, c].gameObject.transform.localPosition.y);
            return new Vector2(cells[r, c].gameObject.transform.localPosition.x + grid.cellSize.x / 2 * (itemCols - 1), cells[r, c].gameObject.transform.localPosition.y);
        }

        private bool CanPlace(int[,] shape, int startX, int startY, GridInfor grid)
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

        public void ChangeStatusCell(InventoryItemData inventoryItemData, int startX, int startY, Vector2 pos, GridInfor grid)
        {
            var shape = Shape.ShapeDic[inventoryItemData.shapeId];
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);

            for (int i = 0; i < itemRows; i++)
            {
                for (int j = 0; j < itemCols; j++)
                {
                    grid.cells[startX + i, startY + j].SetStatusCell(StatusCell.Occupied);
                }
            }
        }


    }

}