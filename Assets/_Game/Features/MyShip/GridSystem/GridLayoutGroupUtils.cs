using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Features.MyShip.GridSystem
{
    public static class GridLayoutGroupUtils
    {
        /// <summary>
        /// Calculates the number of items in one row of the specified GridLayoutGroup.
        /// </summary>
        /// <param name="gridLayoutGroup">The GridLayoutGroup to calculate for.</param>
        /// <returns>The number of items in one row.</returns>
        public static int GetItemsPerRow(GridLayoutGroup gridLayoutGroup)
        {
            var rectTransform = gridLayoutGroup.GetComponent<RectTransform>();
            // LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

            // Calculate the total width available for cells
            var totalWidth = rectTransform.sizeDelta.x - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right;

            // Calculate the width of a single cell including its spacing
            var cellWidthWithSpacing = gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x;

            // Calculate the number of cells that fit in the total width
            var itemsPerRow = Mathf.FloorToInt(totalWidth / cellWidthWithSpacing);

            return itemsPerRow;
        }

        /// <summary>
        /// Calculates the number of rows in the specified GridLayoutGroup.
        /// </summary>
        /// <param name="gridLayoutGroup">The GridLayoutGroup to calculate for.</param>
        /// <returns>The number of rows.</returns>
        public static int GetRowCount(GridLayoutGroup gridLayoutGroup)
        {
            var rectTransform = gridLayoutGroup.GetComponent<RectTransform>();
            var childCount = gridLayoutGroup.transform.childCount;
            var columnCount = GetItemsPerRow(gridLayoutGroup);

            if (columnCount == 0)
                return 0;

            var rowCount = Mathf.CeilToInt((float)childCount / columnCount);
            return rowCount;
        }
        
        /// <summary>
        /// Gets the column and row of a child by its sibling index in the specified GridLayoutGroup.
        /// </summary>
        /// <param name="gridLayoutGroup">The GridLayoutGroup to calculate for.</param>
        /// <param name="siblingIndex">The sibling index of the child.</param>
        /// <param name="column">Output parameter for the column index.</param>
        /// <param name="row">Output parameter for the row index.</param>
        public static void GetCellPosition(GridLayoutGroup gridLayoutGroup, int siblingIndex, out int column, out int row)
        {
            var itemsPerRow = GetItemsPerRow(gridLayoutGroup);

            column = siblingIndex % itemsPerRow;
            row = siblingIndex / itemsPerRow;
        }
        
        /// <summary>
        /// Gets the column and row of a child by its sibling index in the specified GridLayoutGroup.
        /// </summary>
        /// <param name="gridLayoutGroup">The GridLayoutGroup to calculate for.</param>
        /// <param name="siblingIndex">The sibling index of the child.</param>
        /// <returns>A Vector2Int where x is the column and y is the row.</returns>
        public static Vector2Int GetCellPosition(GridLayoutGroup gridLayoutGroup, int siblingIndex)
        {
            int itemsPerRow = GetItemsPerRow(gridLayoutGroup);

            int column = siblingIndex % itemsPerRow;
            int row = siblingIndex / itemsPerRow;

            return new Vector2Int(column, row);
        }
        
        /// <summary>
        /// Gets the world position of a cell by its sibling index in the specified GridLayoutGroup.
        /// </summary>
        public static Vector3 GetCellWorldPosition(GridLayoutGroup gridLayoutGroup, int siblingIndex)
        {
            RectTransform rectTransform = gridLayoutGroup.GetComponent<RectTransform>();
            Vector2 cellSize = gridLayoutGroup.cellSize;
            Vector2 spacing = gridLayoutGroup.spacing;
            RectOffset padding = gridLayoutGroup.padding;

            Vector2Int position = GetCellPosition(gridLayoutGroup, siblingIndex);
            float x = padding.left + position.x * (cellSize.x + spacing.x);
            float y = -(padding.top + position.y * (cellSize.y + spacing.y));

            Vector3 localPosition = new Vector3(x, y, 0);
            Vector3 worldPosition = rectTransform.TransformPoint(localPosition);

            return worldPosition;
        }
        
        /// <summary>
        /// Gets the sibling index of a child from its position in the GridLayoutGroup.
        /// </summary>
        public static int GetSiblingIndex(GridLayoutGroup gridLayoutGroup, Vector2Int position)
        {
            int itemsPerRow = GetItemsPerRow(gridLayoutGroup);
            if (position.x >= itemsPerRow)
            {
                return -1;
            }
            int siblingIndex = (position.y * itemsPerRow) + position.x;

            if (siblingIndex >= gridLayoutGroup.transform.childCount)
            {
                return -1;
            }
            
            return siblingIndex;
        }
        
        /// <summary>
        /// Finds the center Vector2Int in a list of Vector2Int by calculating the centroid and finding the closest vector to it.
        /// </summary>
        public static Vector2Int FindCenterVector(List<Vector2Int> vectors)
        {
            if (vectors == null || vectors.Count == 0)
                throw new System.ArgumentException("The list of vectors cannot be null or empty.");

            // Calculate the centroid
            Vector2 centroid = Vector2.zero;
            foreach (Vector2Int vector in vectors)
            {
                centroid += (Vector2)vector;
            }
            centroid /= vectors.Count;

            // Find the closest vector to the centroid
            Vector2Int centerVector = vectors[0];
            float closestDistance = Vector2.Distance(centroid, centerVector);
            foreach (Vector2Int vector in vectors)
            {
                float distance = Vector2.Distance(centroid, vector);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    centerVector = vector;
                }
            }

            return centerVector;
        }
        
        /// <summary>
        /// Gets the child RectTransform at the specified Vector2Int position in the GridLayoutGroup.
        /// </summary>
        public static RectTransform GetCellAtPosition(GridLayoutGroup gridLayoutGroup, Vector2Int position)
        {
            int siblingIndex = GetSiblingIndex(gridLayoutGroup, position);
            if (siblingIndex >= 0 && siblingIndex < gridLayoutGroup.transform.childCount)
            {
                return gridLayoutGroup.transform.GetChild(siblingIndex) as RectTransform;
            }
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridLayout"></param>
        public static Vector2Int GetGridLayoutSize(GridLayoutGroup gridLayout)
        {
            var dimension = Vector2Int.zero;
            
            var rectTransform = gridLayout.GetComponent<RectTransform>();

            if (gridLayout != null)
            {
                // Calculate the number of columns based on the width of the grid and cell size
                float cellWidth = gridLayout.cellSize.x + gridLayout.spacing.x;
                dimension.x = Mathf.FloorToInt(rectTransform.rect.width / cellWidth);

                // Calculate the number of rows based on the height of the grid and cell size
                float cellHeight = gridLayout.cellSize.y + gridLayout.spacing.y;
                dimension.y = Mathf.FloorToInt(rectTransform.rect.height / cellHeight);
            }
            else
            {
                Debug.LogError("GridLayoutGroup component is null.");
            }

            return dimension;
        }
    }
}