
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


        private LineRenderer[] lineRenderers;
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
            lineRenderers = new LineRenderer[col + row + 2]; // One for horizontal lines, one for vertical lines

            // Calculate grid dimensions
            float width = col * spaces.x;
            float height = row * spaces.y;

            Vector3 startPosition = transform.position - new Vector3(width / 2, height / 2, 0);

            // Create horizontal lines
            for (int i = 0; i <= row; i++)
            {
                Vector3 start = startPosition + new Vector3(0, i * spaces.y, 0);
                Vector3 end = start + new Vector3(width, 0, 0);
                CreateLineRenderer(i, start, end);
            }

            // Create vertical lines
            for (int j = 0; j <= col; j++)
            {
                Vector3 start = startPosition + new Vector3(j * spaces.x + 0.5f, 0, 0);
                Vector3 end = start + new Vector3(0, height, 0);
                CreateLineRenderer(j + row + 1, start, end);
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

        private void CreateLineRenderer(int index, Vector3 start, Vector3 end)
        {
            GameObject lineObject = new GameObject("GridLine" + index);
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

            lineRenderers[index] = lineRenderer;
        }
    }
}
