
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShipGridDraw : MonoBehaviour
{
        public GridVisual gridVisual;
        public float lineWidth = 0.05f;
        public float cellWidth = 1.0f;  // Default value, you can change it as needed
        public float cellHeight = 1.0f; // Default value, you can change it as needed

        private void OnDrawGizmos()
        {
            DrawGridGizmos();
        }

        private void DrawGridGizmos()
        {
            if (gridVisual == null || gridVisual.col <= 0 || gridVisual.row <= 0)
                return;

            Gizmos.color = UnityEngine.Color.green;

            Bounds bounds = gridVisual.bounds;
            int columns = gridVisual.col;
            int rows = gridVisual.row;

            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            // Draw vertical lines
            for (int i = 0; i <= columns; i++)
            {
                Vector3 start = new Vector3(min.x + i * cellWidth, min.y, 0);
                Vector3 end = new Vector3(min.x + i * cellWidth, max.y, 0);
                Gizmos.DrawLine(start, end);
            }

            // Draw horizontal lines
            for (int j = 0; j <= rows; j++)
            {
                Vector3 start = new Vector3(min.x, min.y + j * cellHeight, 0);
                Vector3 end = new Vector3(max.x, min.y + j * cellHeight, 0);
                Gizmos.DrawLine(start, end);
            }
        }

        public Material lineMaterial;
        private List<LineRenderer> lineRenderers = new List<LineRenderer>();

        private void OnEnable()
        {
            CreateGrid();
        }

        private void OnDisable()
        {
            //ClearGrid();
        }

        private void CreateGrid()
        {
            if (gridVisual == null || gridVisual.col <= 0 || gridVisual.row <= 0)
                return;

            Bounds bounds = gridVisual.bounds;
            int columns = gridVisual.col;
            int rows = gridVisual.row;

            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            // Draw vertical lines
            for (int i = 0; i <= columns; i++)
            {
                Vector3 start = new Vector3(min.x + i * cellWidth, min.y, 0);
                Vector3 end = new Vector3(min.x + i * cellWidth, max.y, 0);
                CreateLineRenderer(start, end);
            }

            // Draw horizontal lines
            for (int j = 0; j <= rows; j++)
            {
                Vector3 start = new Vector3(min.x, min.y + j * cellHeight, 0);
                Vector3 end = new Vector3(max.x, min.y + j * cellHeight, 0);
                CreateLineRenderer(start, end);
            }
        }

        private void ClearGrid()
        {
            foreach (var lineRenderer in lineRenderers)
            {
                if (lineRenderer != null)
                {
                    DestroyImmediate(lineRenderer.gameObject);
                }
            }
            lineRenderers.Clear();
        }

        private void CreateLineRenderer(Vector3 start, Vector3 end)
        {
            GameObject lineObject = new GameObject("GridLine");
            lineObject.transform.SetParent(transform);
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            lineRenderer.startColor = gridVisual.color;
            lineRenderer.endColor = gridVisual.color;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            lineRenderer.material = lineMaterial;
            lineRenderer.useWorldSpace = false;

            lineRenderers.Add(lineRenderer);
        }
    }


