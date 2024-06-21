using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SortOrderByYPos : MonoBehaviour
{
    MeshRenderer partRenderer;
    public Transform sortPivot;
    private void Start()
    {
        partRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        partRenderer.sortingOrder = Mathf.RoundToInt(sortPivot.position.y * -100);
    }
}
