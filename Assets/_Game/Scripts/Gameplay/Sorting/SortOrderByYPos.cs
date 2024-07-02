using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class SortOrderByYPos : MonoBehaviour
{
    MeshRenderer partRenderer;
    SortingGroup sortingGroup;
    public Transform sortPivot;
    private void Start()
    {
        partRenderer = GetComponent<MeshRenderer>();
        sortingGroup = GetComponent<SortingGroup>();
    }
    private void Update()
    {
        if (partRenderer != null)
        {

            partRenderer.sortingOrder = Mathf.RoundToInt(sortPivot.position.y * -100);
        }
        else
        {
            sortingGroup.sortingOrder = Mathf.RoundToInt(sortPivot.position.y * -100);
        }

    }
}
