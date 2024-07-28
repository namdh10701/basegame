using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetSortOrderHUD : MonoBehaviour
{
    public MeshRenderer target;
    public Canvas canvas;

    private void Update()
    {
        canvas.sortingOrder = target.sortingOrder + 1;
        canvas.sortingLayerName = target.sortingLayerName;
    }
}
