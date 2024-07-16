using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class NodeGraphGenerator : MonoBehaviour
{
    public Node nodePrefab;  // Prefab to instantiate
    public int width = 5;          // Number of nodes along the width
    public int height = 5;         // Number of nodes along the height
    public float spacingY = 1.4f;   // Spacing between nodes
    public float spacingX = 1.6f;
    private void OnEnable()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        Node[,] grid = new Node[width, height]; // Create a 2D array to hold node references

        // Instantiate nodes and fill the grid array
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * spacingX, y * spacingY, 0);
                Node nodeObject = Instantiate(nodePrefab, position, Quaternion.identity, transform);
                grid[x, y] = nodeObject;
            }
        }

        // Connect neighbors for each node
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y].ConnectNeighbors(grid, x, y, width, height);
            }
        }
    }
}
