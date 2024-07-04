using System.Collections.Generic;
using System.Linq;
using _Game.Scripts;
using UnityEngine;
namespace _Base.Scripts.UI
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] public GridConfig GridConfig;
        [SerializeField] public List<RectTransform> ParentCells;
        [SerializeField] private List<Transform> _startPos;
        [SerializeField] public RectTransform m_rect;

        [SerializeField] GameObject _tash;
        [SerializeField] GameObject _gridImage;
        [SerializeField] InventoryItem _prefabInventoryItem;

        public List<InventoryItem> InventoryItems = new List<InventoryItem>();
        public List<InventoryItem> InventoryItemsOnGrid = new List<InventoryItem>();
        public List<InventoryItem> InventoryItemsOnStash = new List<InventoryItem>();
        private string _shipID;

        void Awake()
        {
            RemoveAllInventoryItems();
        }


        public void LoadInventoryItems()
        {
            for (int i = 0; i < GridConfig.grids.Count; i++)
            {
                if (GridConfig.grids[i].ItemsReceived.InventoryItemsData.Count > 0)
                    LoadInventoryItemsOnGrid(GridConfig.grids[i].ItemsReceived.InventoryItemsData, GridConfig.grids[i], ParentCells[i]);
            }
        }

        public void EnableDragItem(bool enable)
        {
            foreach (var item in InventoryItems)
            {
                if (item != null)
                    item.Icon.raycastTarget = enable;
            }
            _tash.SetActive(enable);
            _gridImage.SetActive(enable);

        }

        public void FillInventoryItems()
        {
            InventoryItemsOnGrid = InventoryItems.Where(item => item != null && item.GetInventorInfo().gridID == "Ship_1").ToList();
            InventoryItemsOnStash = InventoryItems.Where(item => item != null && item.GetInventorInfo().gridID == "Stash").ToList();
        }

        public void Initialize(string shipID)
        {
            _shipID = shipID;

            for (int i = 0; i < GridConfig.grids.Count; i++)
            {
                var grid = GridConfig.grids[i];
                grid.startPos = _startPos[i];
                grid.cells = new Cell[grid.rows, grid.cols];
                if (grid.listCellsData.Count > 0)
                {
                    for (int r = 0; r < grid.rows; r++)
                    {
                        for (int c = 0; c < grid.cols; c++)
                        {
                            var cell = Instantiate<Cell>(GridConfig.cell, ParentCells[i]);
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
                            var cell = Instantiate<Cell>(GridConfig.cell, ParentCells[i]);
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

            if (_shipID == "0001")
            {
                foreach (var item in GridConfig.grids[0].listCellsData)
                {
                    if ((item.r == 0 && item.c == 3) || (item.r == 2 && item.c == 3))
                    {
                        item.statusCell = StatusCell.Occupied;
                    }
                }
                GridConfig.grids[1].cells[0, 3].SetStatusCell(StatusCell.Occupied);
                GridConfig.grids[1].cells[2, 3].SetStatusCell(StatusCell.Occupied);
            }

        }

        private void LoadCellData(GridInfor grid)
        {
            for (int i = 0; i < grid.listCellsData.Count; i++)
            {
                grid.cells[grid.listCellsData[i].r, grid.listCellsData[i].c].Setup(grid.listCellsData[i]);
            }

        }

        public void AddInventoryItemsInfo(List<InventoryItemData> inventoryItems)
        {
            foreach (var inventoryItem in inventoryItems)
            {
                if (CheckPlaceItem(inventoryItem, GridConfig.grids[1]))
                {
                    inventoryItem.gridID = GridConfig.grids[1].id;
                    GridConfig.grids[1].ItemsReceived.InventoryItemsData.Add(inventoryItem);
                }
                else
                {
                    Debug.Log("Can't insert item inventory");
                }
                continue;
            }
        }

        public void AddInventoryItemsInfoEndDrag(InventoryItemData inventoryItemData, GridInfor gridInfor)
        {
            foreach (var grid in GridConfig.grids)
            {
                if (grid.id == gridInfor.id)
                {
                    grid.ItemsReceived.InventoryItemsData.Add(inventoryItemData);
                }
            }
        }

        public void SubmitItemInventorToGamePlay()
        {
            var GridItemDatas = new List<GridItemData>();
            foreach (var item in GridConfig.grids[0].ItemsReceived.InventoryItemsData)
            {
                var GridItemData = new GridItemData();
                GridItemData.Id = item.Id;
                GridItemData.GridId = "1";
                GridItemData.position = Vector3.zero;
                GridItemData.OccupyCells = new List<Vector2Int>();
                GridItemData.startX = item.startY;
                GridItemData.startY = item.startX;


                switch (item.Type)
                {
                    case _Game.Features.Inventory.ItemType.CREW:
                        GridItemData.GridItemType = GridItemType.Crew;
                        break;
                    case _Game.Features.Inventory.ItemType.CANNON:
                        GridItemData.GridItemType = GridItemType.Cannon;
                        break;
                    case _Game.Features.Inventory.ItemType.AMMO:
                        GridItemData.GridItemType = GridItemType.Bullet;
                        break;
                }
                GridItemDatas.Add(GridItemData);
            }
            if (_shipID == "0003")
            {
                ShipSetup.GridItemDatas = GridItemDatas;
            }
            if (_shipID == "0001")
            {
                ShipSetup.GridItemDatas_Id2 = GridItemDatas;
            }

        }

        public void RemoveInventoryItemsInfoEndDrag(InventoryItemData inventoryItemData, GridInfor gridInfor)
        {
            foreach (var grid in GridConfig.grids)
            {
                if (grid.id == gridInfor.id)
                {
                    grid.ItemsReceived.InventoryItemsData.Remove(inventoryItemData);
                }
            }

        }

        public void RemoveInventoryItemsInfo(List<InventoryItemData> InventoryItemsData, GridInfor grid)
        {
            var itemsToRemove = new List<InventoryItemData>();

            foreach (var inventoryItem in InventoryItemsData)
            {
                foreach (var inventoryItemData in grid.ItemsReceived.InventoryItemsData)
                {
                    if (inventoryItem == inventoryItemData)
                    {
                        itemsToRemove.Add(inventoryItemData);
                    }
                }
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                grid.ItemsReceived.InventoryItemsData.Remove(itemToRemove);
                ChangeStatusCell(itemToRemove, itemToRemove.startX, itemToRemove.startY, grid, StatusCell.Empty);
            }
        }


        public void LoadInventoryItemsOnGrid(List<InventoryItemData> InventoryItemsData, GridInfor grid, Transform parent)
        {
            RemoveAllInventoryItemsOnStash();
            foreach (var inventoryItem in InventoryItemsData)
            {
                inventoryItem.gridID = grid.id;
                var item = Instantiate<InventoryItem>(_prefabInventoryItem, parent);
                item.Setup(inventoryItem);
                if (grid.id == "Ship_1")
                    InventoryItemsOnGrid.Add(item);
                else
                    InventoryItemsOnStash.Add(item);

            }
            foreach (var item in InventoryItemsOnGrid)
            {
                InventoryItems.Add(item);
            }
            foreach (var item in InventoryItemsOnStash)
            {
                InventoryItems.Add(item);
            }
        }

        private void RemoveAllInventoryItemsOnStash()
        {
            // Dùng danh sách tạm thời để lưu các mục sẽ bị hủy
            List<InventoryItem> itemsToRemove = new List<InventoryItem>();

            // Kiểm tra và thêm vào danh sách tạm thời chỉ khi đối tượng chưa bị hủy
            foreach (var item in InventoryItemsOnStash)
            {
                if (item != null && item.gameObject != null)
                {
                    itemsToRemove.Add(item);
                }
            }

            // Hủy tất cả các đối tượng trong danh sách tạm thời và xóa chúng khỏi danh sách gốc
            foreach (var item in itemsToRemove)
            {
                InventoryItemsOnStash.Remove(item);
                Destroy(item.gameObject);
            }
        }



        public void RemoveAllInventoryItems()
        {
            // Create temporary lists to hold the items to be destroyed
            List<GameObject> itemsToDestroy = new List<GameObject>();

            foreach (var item in InventoryItemsOnGrid)
            {
                if (item != null)
                {
                    itemsToDestroy.Add(item.gameObject);
                }
            }

            foreach (var item in InventoryItemsOnStash)
            {
                if (item != null)
                {
                    itemsToDestroy.Add(item.gameObject);
                }
            }

            // Destroy all items in the temporary list
            foreach (var item in itemsToDestroy)
            {
                Destroy(item);
            }

            // Clear the original lists to remove references to destroyed objects
            InventoryItemsOnGrid.Clear();
            InventoryItemsOnStash.Clear();

            // Clear all grid items and cells data
            foreach (var grid in GridConfig.grids)
            {
                grid.ItemsReceived.InventoryItemsData.Clear();
                grid.listCellsData.Clear();
            }
            Initialize(_shipID);
        }


        public bool CheckPlaceItem(InventoryItemData inventoryItemData, GridInfor grid)
        {
            bool itemPlaced = false;

            for (int r = 0; r < grid.rows && !itemPlaced; r++)
            {
                for (int c = 0; c < grid.cols && !itemPlaced; c++)
                {
                    var shape = inventoryItemData.Shape;
                    var result = CanPlace(shape, r, c, grid);
                    if (result.canPlace)
                    {
                        var pos = GetPositionCell(shape, r, c, grid);
                        inventoryItemData.position = pos;
                        ChangeStatusCell(inventoryItemData, r, c, grid, StatusCell.Occupied);
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
            var shape = inventoryItemData.Shape;
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
            foreach (var grid in GridConfig.grids)
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

        public int GetParentIndex(Vector2 mousePosition)
        {
            Vector2 positionInGridManager;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rect, mousePosition, Camera.main, out positionInGridManager);

            int index = -1;

            foreach (var parent in ParentCells)
            {
                if (parent.localPosition.x < positionInGridManager.x &&
                    positionInGridManager.x < parent.localPosition.x + parent.rect.width &&
                    parent.localPosition.y < positionInGridManager.y &&
                    positionInGridManager.y < parent.localPosition.y + parent.rect.height)

                    if (parent.name == "Grid")
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
            return index;
        }


        public (List<Cell> cells, GridInfor grid) GetCellsByPosition(DragItemUIController dragItemUIController, int parentIndex, Vector2 mousePosition, Vector2 inventoryPositon, InventoryItem inventoryItem, bool startDrag = false)
        {

            Vector2 positionInGridManager;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rect, mousePosition, Camera.main, out positionInGridManager);

            dragItemUIController.SetParent(ParentCells[parentIndex]);
            var grid = GridConfig.grids[parentIndex];
            if (startDrag)
            {
                var cell = grid.cells[inventoryItem.GetInventorInfo().startX, inventoryItem.GetInventorInfo().startY];
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
                    var shape = inventoryItem.GetInventorInfo().Shape;
                    if (IsMouseOverCell(inventoryPositon, cell, shape))
                    {
                        var cellData = cell.GetCellData();
                        var result = CanPlace(inventoryItem.GetShape(), cellData.r, cellData.c, grid, true);
                        if (result.canPlace)
                        {
                            inventoryItem.GetInventorInfo().startX = cellData.r;
                            inventoryItem.GetInventorInfo().startY = cellData.c;
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
                foreach (var grid in GridConfig.grids)
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
                foreach (var grid in GridConfig.grids)
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