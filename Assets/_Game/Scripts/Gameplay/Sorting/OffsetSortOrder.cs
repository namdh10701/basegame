using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class OffsetSortOrder : MonoBehaviour
{
    public MeshRenderer TargetSortingGroup;
    public Renderer sortGroup;
    public int offset;
    public SortingGroup sortingGroup;
    private void Update()
    {
        if (sortGroup != null)
        {
            sortGroup.sortingOrder = TargetSortingGroup.sortingOrder + offset;
        }
        else
        {
            sortingGroup.sortingOrder = TargetSortingGroup.sortingOrder + offset;
        }
    }
}
