using System.Collections.Generic;
using _Game.Scripts;
using Unity.VisualScripting;
using UnityEngine;
namespace _Base.Scripts.UI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField] private List<RectTransform> _parentCells;
        [SerializeField] private List<Transform> _startPos;
        [SerializeField] private InventoryItemsConfig _TestinventoryItemsConfig;
        [SerializeField] public RectTransform m_rect;

        void Awake()
        {
            Initialize();
        }

        void OnEnable()
        {
            AddInventoryItemsInfo(_TestinventoryItemsConfig.inventoryItemsInfo, _gridConfig.grids[1]);
            LoadInventoryItemsOnStash(_gridConfig.grids[1].inventoryItemsConfig.inventoryItemsInfo, _gridConfig.grids[1], _parentCells[1]);
        }


        void OnDisable()
        {
            RemoveInventoryItemsInfo(_TestinventoryItemsConfig.inventoryItemsInfo, _gridConfig.grids[1]);
        }

        private void Initialize()
        {
            for (int i = 0; i < _gridConfig.grids.Count; i++)
            {
                var grid = _gridConfig.grids[i];
                grid.startPos = _startPos[i];
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
                            float posX = grid.startPos.localPosition.x + c * (grid.cellSize.x + grid.spacing) + grid.cellSize.x / 2;
                            float posY = grid.startPos.localPosition.y + r * (grid.cellSize.y + grid.spacing);
                            cell.transform.localPosition = new Vector2(posX, posY);
                            grid.cells[r, c] = cell;
                        }
                    }
                    LoadCellData(grid);
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
                            float posX = grid.startPos.localPosition.x + c * (grid.cellSize.x + grid.spacing) + grid.cellSize.x / 2;
                            float posY = grid.startPos.localPosition.y + r * (grid.cellSize.y + grid.spacing);
                            cell.transform.localPosition = new Vector2(posX, posY);
                            grid.cells[r, c] = cell;
                            var cellData = new CellData(r, c, StatusCell.Empty, new Vector2(posX, posY), grid.cellSize);
                            grid.listCellsData.Add(cellData);
                            cell.Setup(cellData);
                        }
                    }
                }

            }

        }

        private void LoadCellData(GridInfor grid)
        {
            for (int i = 0; i < grid.listCellsData.Count; i++)
            {
                grid.cells[grid.listCellsData[i].r, grid.listCellsData[i].c].Setup(grid.listCellsData[i]);
            }

        }

        public void AddInventoryItemsInfo(List<InventoryItemInfo> inventoryItems, GridInfor grid)
        {
            var inventoryIntems = new List<InventoryItemInfo>();
            foreach (var inventoryItem in inventoryItems)
            {
                if (CheckPlaceItem(inventoryItem, grid))
                {
                    grid.inventoryItemsConfig.inventoryItemsInfo.Add(inventoryItem);
                    inventoryIntems.Add(inventoryItem);
                }
                else
                {
                    Debug.Log("Can't insert item inventory");
                }
                continue;
            }
        }

        public void AddInventoryItemsInfoEndDrag(InventoryItemInfo inventoryItemInfo, GridInfor gridInfor)
        {
            foreach (var grid in _gridConfig.grids)
            {
                if (grid.id == gridInfor.id)
                {
                    grid.inventoryItemsConfig.inventoryItemsInfo.Add(inventoryItemInfo);
                }
            }

        }

        public void RemoveInventoryItemsInfo(List<InventoryItemInfo> inventoryItems, GridInfor grid)
        {
            var itemsToRemove = new List<InventoryItemInfo>();

            foreach (var inventoryItem in inventoryItems)
            {
                foreach (var inventoryItemInfo in grid.inventoryItemsConfig.inventoryItemsInfo)
                {
                    if (inventoryItem.inventoryItemData == inventoryItemInfo.inventoryItemData)
                    {
                        itemsToRemove.Add(inventoryItemInfo);
                    }
                }
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                grid.inventoryItemsConfig.inventoryItemsInfo.Remove(itemToRemove);
                ChangeStatusCell(itemToRemove.inventoryItemData, itemToRemove.inventoryItemData.startX, itemToRemove.inventoryItemData.startY, grid, StatusCell.Empty);

            }
        }


        private void LoadInventoryItemsOnStash(List<InventoryItemInfo> inventoryItems, GridInfor grid, Transform parent)
        {
            foreach (var inventoryItem in inventoryItems)
            {
                inventoryItem.inventoryItemData.gridID = grid.id;
                var item = Instantiate<InventoryItem>(grid.inventoryItemsConfig.InventoryItem, parent);
                item.Setup(inventoryItem);
            }
        }

        public bool CheckPlaceItem(InventoryItemInfo inventoryItemInfo, GridInfor grid)
        {
            bool itemPlaced = false;

            for (int r = 0; r < grid.rows && !itemPlaced; r++)
            {
                for (int c = 0; c < grid.cols && !itemPlaced; c++)
                {
                    var shape = Shape.ShapeDic[inventoryItemInfo.inventoryItemData.shapeId];
                    var result = CanPlace(shape, r, c, grid);
                    if (result.canPlace)
                    {
                        var pos = GetPositionCell(shape, r, c, grid);
                        inventoryItemInfo.inventoryItemData.position = pos;
                        ChangeStatusCell(inventoryItemInfo.inventoryItemData, r, c, grid, StatusCell.Occupied);
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

        public Vector2 GetPositionCell(int[,] shape, int r, int c, GridInfor grid)
        {
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);

            var temp = new Vector2(grid.cells[r, c].gameObject.transform.localPosition.x + grid.cellSize.x / 2 * (itemCols - 1),
             grid.cells[r, c].gameObject.transform.localPosition.y);

            return new Vector2(grid.cells[r, c].gameObject.transform.localPosition.x + grid.cellSize.x / 2 * (itemCols - 1) + grid.spacing / 2 * (itemCols - 1),
             grid.cells[r, c].gameObject.transform.localPosition.y);
        }

        private (bool canPlace, List<Cell> blockedCells) CanPlace(int[,] shape, int startX, int startY, GridInfor grid, bool isStartDrag = false)
        {
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);

            var cells = new List<Cell>();

            if (startX + itemRows > grid.rows || startY + itemCols > grid.cols)
            {
                return (false, cells);
            }

            for (int i = 0; i < itemRows; i++)
            {
                for (int j = 0; j < itemCols; j++)
                {
                    if (!isStartDrag)
                    {
                        if (shape[i, j] == 1 && grid.cells[startX + i, startY + j].CheckCellEmty())
                        {
                            cells.Add(grid.cells[startX + i, startY + j]);
                        }
                        else if (shape[i, j] == 1)
                        {
                            return (false, new List<Cell>());
                        }
                    }
                    else
                    {
                        cells.Add(grid.cells[startX + i, startY + j]);
                    }
                }
            }

            return (true, cells);
        }

        public void ChangeStatusCell(InventoryItemData inventoryItemData, int startX, int startY, GridInfor grid, StatusCell statusCell)
        {
            var shape = Shape.ShapeDic[inventoryItemData.shapeId];
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);
            inventoryItemData.startX = startX;
            inventoryItemData.startY = startY;
            inventoryItemData.gridID = grid.id;

            for (int i = 0; i < itemRows; i++)
            {
                for (int j = 0; j < itemCols; j++)
                {
                    grid.cells[startX + i, startY + j].SetStatusCell(statusCell);
                }
            }

        }

        public void ChangeStatusCellEndDrag(List<Cell> cells, GridInfor gridInfor, StatusCell statusCell)
        {
            foreach (var grid in _gridConfig.grids)
            {
                if (grid.id == gridInfor.id)
                {
                    for (int i = 0; i < cells.Count; i++)
                    {
                        grid.cells[cells[i].GetCellData().r, cells[i].GetCellData().c].GetCellData().statusCell = statusCell;
                    }
                }
            }
        }


        public (List<Cell> cells, GridInfor grid) GetCellsByPosition(DragItemUIController dragItemUIController, Vector2 mousePosition, Vector2 inventoryPositon, InventoryItem inventoryItem, bool startDrag = false)
        {

            Vector2 positionInGridManager;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rect, mousePosition, Camera.main, out positionInGridManager);

            int index = -1;

            foreach (var parent in _parentCells)
            {
                if (parent.localPosition.x < positionInGridManager.x &&
                    positionInGridManager.x < parent.localPosition.x + parent.rect.width &&
                    parent.localPosition.y < positionInGridManager.y &&
                    positionInGridManager.y < parent.localPosition.y + parent.rect.height)

                    if (parent.name == "Ship")
                    {
                        index = 0;
                        break;
                    }
                    else if (parent.name == "Stash")
                    {
                        index = 1;
                        break;
                    }
            }

            // Nếu không tìm thấy chỉ mục hợp lệ, trả về null
            if (index == -1)
            {
                return (new List<Cell>(), null);
            }
            
            dragItemUIController.SetParent(_parentCells[index]);
            var grid = _gridConfig.grids[index];
            if (startDrag)
            {
                var cell = grid.cells[inventoryItem.GetInventorInfo().inventoryItemData.startX, inventoryItem.GetInventorInfo().inventoryItemData.startY];
                var cellData = cell.GetCellData();
                Debug.Log($"[GetCellByPosition]: {cellData.r}, {cellData.c}");
                var result = CanPlace(inventoryItem.GetShape(), cellData.r, cellData.c, grid, true);
                if (result.canPlace)
                {
                    for (int i = 0; i < result.blockedCells.Count; i++)
                    {
                        grid.cells[result.blockedCells[i].GetCellData().r, result.blockedCells[i].GetCellData().c].SetStatusCell(StatusCell.Empty);

                    }
                    return (result.blockedCells, grid);
                }
            }

            for (int r = 0; r < grid.rows; r++)
            {
                for (int c = 0; c < grid.cols; c++)
                {
                    var cell = grid.cells[r, c];
                    var shape = Shape.ShapeDic[inventoryItem.GetInventorInfo().inventoryItemData.shapeId];
                    if (IsMouseOverCell(inventoryPositon, cell, shape))
                    {
                        var cellData = cell.GetCellData();
                        var result = CanPlace(inventoryItem.GetShape(), cellData.r, cellData.c, grid, true);
                        if (result.canPlace)
                        {
                            inventoryItem.GetInventorInfo().inventoryItemData.startX = cellData.r;
                            inventoryItem.GetInventorInfo().inventoryItemData.startY = cellData.c;
                            return (result.blockedCells, grid);
                        }
                    }
                    // return (new List<Cell>(), grid);
                }
            }
            return (new List<Cell>(), grid);

        }

        private bool IsMouseOverCell(Vector2 position, Cell cell, int[,] shape)
        {
            int itemRows = shape.GetLength(0);
            int itemCols = shape.GetLength(1);

            var cellData = cell.GetCellData();
            var cellPosition = cellData.position;
            var cellSize = cellData.size;

            // Tính toán biên giới hạn của cell
            float cellMinX = cellPosition.x - cellSize.x * itemCols / 2;
            float cellMaxX = cellPosition.x + cellSize.x * itemCols / 2;
            float cellMinY = cellPosition.y;
            float cellMaxY = cellPosition.y + cellSize.y;

            // Kiểm tra nếu vị trí chuột nằm trong giới hạn của cell và vượt qua 1/2 cell kế bên
            bool withinCellX = position.x >= cellMinX &&
                               position.x <= cellMaxX;
            bool withinCellY = position.y >= cellMinY &&
                               position.y <= cellMaxY;

            return withinCellX && withinCellY;
        }

        public void OnDragChangeColorCells(List<Cell> cells, GridInfor gridInfor, bool isPreviousCells = false)
        {
            if (isPreviousCells)
            {
                foreach (var grid in _gridConfig.grids)
                {
                    if (grid.id == gridInfor.id)
                    {
                        for (int i = 0; i < cells.Count; i++)
                        {
                            var c = Color.white;
                            c.a = 0;
                            grid.cells[cells[i].GetCellData().r, cells[i].GetCellData().c].ChangeColor(c);
                        }
                    }
                }
            }
            else
            {
                foreach (var grid in _gridConfig.grids)
                {
                    if (grid.id == gridInfor.id)
                    {
                        for (int i = 0; i < cells.Count; i++)
                        {
                            if (grid.cells[cells[i].GetCellData().r, cells[i].GetCellData().c].GetStatusCell() == StatusCell.Empty)
                            {
                                Debug.Log("OnDragChangeColorCells: GREEB ");
                                grid.cells[cells[i].GetCellData().r, cells[i].GetCellData().c].ChangeColor(Color.green);
                            }
                            else
                            {
                                Debug.Log("OnDragChangeColorCells:RED ");
                                grid.cells[cells[i].GetCellData().r, cells[i].GetCellData().c].ChangeColor(Color.red);
                            }
                        }
                    }
                }
            }

        }


    }

}