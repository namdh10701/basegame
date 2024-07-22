using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Features.Inventory
{
    [ExecuteAlways]
    public class GridDrawer : Graphic
    {
        public float cellWidth = 50f; // Width of each cell in pixels
        public float cellHeight = 50f; // Height of each cell in pixels
        public float strokeWidth = 1f; // Width of the grid lines
        public Color defaultStrokeColor = Color.black; // Default stroke color
        public Color defaultCellColor = Color.clear; // Default cell color (transparent)
        public Color defaultBackgroundColor = Color.white; // Default background color
        public bool defaultCellVisible = true; // Default cell visibility
        public int gridWidth = 10; // Default grid width
        public int gridHeight = 10; // Default grid height

        public bool autoCalculateGridSize = true; // Toggle to automatically calculate grid size

        public List<Vector2Int> invisibleCells; // List of invisible cell positions
        public List<Vector2Int> highlightedCells; // List of highlighted cell positions

        private Cell[,] cells; // 2D array to store cell properties

        public class Cell
        {
            public Color strokeColor;
            public Color cellColor;
            public Color backgroundColor;
            public bool isVisible;

            public Cell(Color strokeColor, Color cellColor, Color backgroundColor, bool isVisible)
            {
                this.strokeColor = strokeColor;
                this.cellColor = cellColor;
                this.backgroundColor = backgroundColor;
                this.isVisible = isVisible;
            }
        }


        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            if (autoCalculateGridSize)
            {
                CalculateGridDimensions();
            }

            InitializeGrid();
        }

        void CalculateGridDimensions()
        {
            // Calculate gridWidth and gridHeight based on RectTransform size
            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;

            gridWidth = Mathf.FloorToInt(width / cellWidth);
            gridHeight = Mathf.FloorToInt(height / cellHeight);
        }

        void InitializeGrid()
        {
            cells = new Cell[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    cells[x, y] = new Cell(defaultStrokeColor, defaultCellColor, defaultBackgroundColor,
                        defaultCellVisible);
                }
            }

            ApplyInvisibleCells();
            ApplyHighlightedCells();
        }

        void ApplyInvisibleCells()
        {
            if (invisibleCells != null)
            {
                foreach (var cellPos in invisibleCells)
                {
                    if (cellPos.x >= 0 && cellPos.x < gridWidth && cellPos.y >= 0 && cellPos.y < gridHeight)
                    {
                        cells[cellPos.x, cellPos.y].isVisible = false;
                    }
                }
            }
        }

        void ApplyHighlightedCells()
        {
            if (highlightedCells != null)
            {
                foreach (var cellPos in highlightedCells)
                {
                    if (cellPos.x >= 0 && cellPos.x < gridWidth && cellPos.y >= 0 && cellPos.y < gridHeight)
                    {
                        // Customize the highlighted cell appearance here
                        cells[cellPos.x, cellPos.y].strokeColor = Color.yellow;
                        cells[cellPos.x, cellPos.y].cellColor = Color.white;
                        cells[cellPos.x, cellPos.y].backgroundColor = Color.cyan;
                    }
                }
            }
        }

        public void UpdateCell(int x, int y, Color? strokeColor = null, Color? cellColor = null,
            Color? backgroundColor = null, bool? isVisible = null)
        {
            if (cells != null && x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            {
                if (strokeColor.HasValue) cells[x, y].strokeColor = strokeColor.Value;
                if (cellColor.HasValue) cells[x, y].cellColor = cellColor.Value;
                if (backgroundColor.HasValue) cells[x, y].backgroundColor = backgroundColor.Value;
                if (isVisible.HasValue) cells[x, y].isVisible = isVisible.Value;
                SetVerticesDirty();
            }
            else
            {
                Debug.LogError($"Invalid cell indices: ({x}, {y})");
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            var width = rectTransform.rect.width;
            var height = rectTransform.rect.height;
            var pivotOffset = new Vector2(rectTransform.pivot.x * width, rectTransform.pivot.y * height);

            FillBackground(vh, width, height, pivotOffset);

            if (cells != null)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    for (int y = 0; y < gridHeight; y++)
                    {
                        if (cells[x, y].isVisible)
                        {
                            DrawCell(vh, x, y, pivotOffset);
                            DrawCellBackground(vh, x, y, pivotOffset);
                            DrawCellBorder(vh, x, y, pivotOffset);
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Cells array is not initialized.");
            }
        }

        void DrawLine(VertexHelper vh, Vector2 start, Vector2 end, Color color)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            Vector2 direction = (end - start).normalized;
            Vector2 perpendicular = new Vector2(-direction.y, direction.x) * strokeWidth * 0.5f;

            vertex.position = start + perpendicular;
            vh.AddVert(vertex);

            vertex.position = start - perpendicular;
            vh.AddVert(vertex);

            vertex.position = end - perpendicular;
            vh.AddVert(vertex);

            vertex.position = end + perpendicular;
            vh.AddVert(vertex);

            int startIndex = vh.currentVertCount - 4;
            vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
            vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
        }

        void FillBackground(VertexHelper vh, float width, float height, Vector2 pivotOffset)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = Color.clear;

            vertex.position = new Vector2(-pivotOffset.x - strokeWidth * 0.5f, -pivotOffset.y - strokeWidth * 0.5f);
            vh.AddVert(vertex);

            vertex.position = new Vector2(-pivotOffset.x - strokeWidth * 0.5f,
                height - pivotOffset.y + strokeWidth * 0.5f);
            vh.AddVert(vertex);

            vertex.position = new Vector2(width - pivotOffset.x + strokeWidth * 0.5f,
                height - pivotOffset.y + strokeWidth * 0.5f);
            vh.AddVert(vertex);

            vertex.position = new Vector2(width - pivotOffset.x + strokeWidth * 0.5f,
                -pivotOffset.y - strokeWidth * 0.5f);
            vh.AddVert(vertex);

            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }

        void DrawCell(VertexHelper vh, int x, int y, Vector2 pivotOffset)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = cells[x, y].cellColor;

            float xPos = x * cellWidth - pivotOffset.x;
            float yPos = y * cellHeight - pivotOffset.y;

            vertex.position = new Vector2(xPos, yPos);
            vh.AddVert(vertex);

            vertex.position = new Vector2(xPos, yPos + cellHeight);
            vh.AddVert(vertex);

            vertex.position = new Vector2(xPos + cellWidth, yPos + cellHeight);
            vh.AddVert(vertex);

            vertex.position = new Vector2(xPos + cellWidth, yPos);
            vh.AddVert(vertex);

            int startIndex = vh.currentVertCount - 4;
            vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
            vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
        }

        void DrawCellBackground(VertexHelper vh, int x, int y, Vector2 pivotOffset)
        {
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = cells[x, y].backgroundColor;

            float xPos = x * cellWidth - pivotOffset.x;
            float yPos = y * cellHeight - pivotOffset.y;

            vertex.position = new Vector2(xPos, yPos);
            vh.AddVert(vertex);

            vertex.position = new Vector2(xPos, yPos + cellHeight);
            vh.AddVert(vertex);

            vertex.position = new Vector2(xPos + cellWidth, yPos + cellHeight);
            vh.AddVert(vertex);

            vertex.position = new Vector2(xPos + cellWidth, yPos);
            vh.AddVert(vertex);

            int startIndex = vh.currentVertCount - 4;
            vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
            vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
        }

        void DrawCellBorder(VertexHelper vh, int x, int y, Vector2 pivotOffset)
        {
            float xPos = x * cellWidth - pivotOffset.x;
            float yPos = y * cellHeight - pivotOffset.y;

            DrawLine(vh, new Vector2(xPos, yPos), new Vector2(xPos, yPos + cellHeight),
                cells[x, y].strokeColor); // Left
            DrawLine(vh, new Vector2(xPos, yPos), new Vector2(xPos + cellWidth, yPos),
                cells[x, y].strokeColor); // Bottom
            DrawLine(vh, new Vector2(xPos + cellWidth, yPos), new Vector2(xPos + cellWidth, yPos + cellHeight),
                cells[x, y].strokeColor); // Right
            DrawLine(vh, new Vector2(xPos, yPos + cellHeight), new Vector2(xPos + cellWidth, yPos + cellHeight),
                cells[x, y].strokeColor); // Top
        }
    }
}