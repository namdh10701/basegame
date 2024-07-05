using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetSortOrderSprite : MonoBehaviour
{
    public SpriteRenderer target;
    public Canvas canvas;

    private void Update()
    {
        canvas.sortingOrder = target.sortingOrder + 1;
    }
}
