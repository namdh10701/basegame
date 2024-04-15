using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private GridData _gridData;
    public string ID;

    public void Setup(GridData gridData)
    {
        ID = gridData.id;
        _gridData = gridData;
    }
}
