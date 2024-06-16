
using System.Collections.Generic;
using UnityEngine;
using static Fusion.Sockets.NetBitBuffer;

namespace _Game.Scripts
{
    public class GridDraw : MonoBehaviour
    {
        [SerializeField] Bounds bounds;
        [SerializeField] Color color;
        public Color playgroundColor;
        public int col;
        public int row;
        public Vector2 spaces;
        public float lineWidth = 0.05f;
        public Material lineMaterial;
        private List<LineRenderer> lineRenderers;
        private void OnDrawGizmos()
        {
            Gizmos.color = playgroundColor; // Set the color of the bounds
            bounds.center = transform.position;
            Gizmos.DrawWireCube(bounds.center, bounds.size);

        }
        private void OnEnable()
        {
            CreateGrid();
        }

        private void OnDisable()
        {
            ClearGrid();
        }

        private void CreateGrid()
        {
            lineRenderers = new List<LineRenderer>(); // One for horizontal lines, one for vertical lines
            // Create vertical lines
            for (int j = -15; j <= 15; j++)
            {
                Vector3 start = new Vector3(j, -20, 0);
                Vector3 end = new Vector3(j, 20, 0);
                LineRenderer line = CreateLineRenderer(start, end);
                lineRenderers.Add(line);
            }


            for (int i = -20; i <= 20; i++)
            {
                Vector3 start = new Vector3(-20, i, 0);
                Vector3 end = new Vector3(20, i, 0);
                LineRenderer line = CreateLineRenderer(start, end);
                lineRenderers.Add(line);
            }
        }

        private void ClearGrid()
        {
            if (lineRenderers != null)
            {
                foreach (var lineRenderer in lineRenderers)
                {
                    if (lineRenderer != null)
                    {
                        Destroy(lineRenderer.gameObject);
                    }
                }
            }
        }

        private LineRenderer CreateLineRenderer(Vector3 start, Vector3 end)
        {
            GameObject lineObject = new GameObject("GridLine");
            lineObject.transform.SetParent(transform);
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.material = lineMaterial;
            return lineRenderer;
        }
    }
}
