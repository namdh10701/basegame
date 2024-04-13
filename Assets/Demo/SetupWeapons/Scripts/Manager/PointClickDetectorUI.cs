using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointClickDetectorUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isDown;
    private float timeThreshold = 1.0f;
    private float elapsedTime = 0f;
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
        var go = eventData.pointerEnter;
        Debug.Log("OnPointerEnter" + go.name);


    }

    private GameObject OnPointerEnter(PointerEventData eventData)
    {
        var go = eventData.pointerClick.gameObject;
        Debug.Log("OnPointerEnter" + go.name);
        return go;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDown = false;
        Debug.Log("OnPointerUp" + isDown);
    }

    void Update()
    {
        if (isDown)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeThreshold)
            {
                Debug.Log("Create model drag");
                isDown = false;
                elapsedTime = 0f;
            }
        }
    }


}
