using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSorting : MonoBehaviour
{
    [SerializeField] MeshRenderer mainRenderer;
    [SerializeField] Transform refer;
    [SerializeField] ICannonVisualElement[] cannonVisualElements;
    [SerializeField] Transform sortPivot;
    // Update is called once per frame
    private void Start()
    {
        cannonVisualElements = GetComponentsInChildren<ICannonVisualElement>();
    }
    void Update()
    {
        if (refer.rotation.eulerAngles.z > 90 && refer.rotation.eulerAngles.z < 270)
        {
            mainRenderer.sortingLayerName = "AboveShipFront";
        }
        else
        {
            mainRenderer.sortingLayerName = "OnShip";
        }

        mainRenderer.sortingOrder = Mathf.RoundToInt(sortPivot.position.y * -100);
        foreach (ICannonVisualElement cannonVisualElement in cannonVisualElements)
        {
            cannonVisualElement.UpdateSorting(mainRenderer);
        }
    }
}
