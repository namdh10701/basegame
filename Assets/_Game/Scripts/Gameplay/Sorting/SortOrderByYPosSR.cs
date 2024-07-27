using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SortOrderByYPosSR : MonoBehaviour
{
    SpriteRenderer sr;
    public Transform sortPivot;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (sr != null)
        {

            sr.sortingOrder = Mathf.RoundToInt(sortPivot.position.y * -100);
        }

    }
}
