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
    private void Update()
    {
        sortGroup.sortingOrder = TargetSortingGroup.sortingOrder + offset;
    }
}
