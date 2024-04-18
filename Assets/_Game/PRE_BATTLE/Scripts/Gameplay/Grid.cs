using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private GridData _gridData;
    public string ID;
    public Cell[,] Cells;
    public int Col => Cells.GetLength(1);
    public int Row => Cells.GetLength(0);
    public void Setup(GridData gridData)
    {
        ID = gridData.id;
        _gridData = gridData;
    }
}
