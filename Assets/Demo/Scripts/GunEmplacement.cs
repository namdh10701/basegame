using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunEmplacement : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Onclick");
    }
}
