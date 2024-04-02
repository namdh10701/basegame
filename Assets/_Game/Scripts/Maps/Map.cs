using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Node[,] Floors;


    public void Init(Vector2Int grid)
    {
        Floors = new Node[grid.x, grid.y];
    }

    public void SelectRandomNodeFloor(int floor)
    {

    }
}