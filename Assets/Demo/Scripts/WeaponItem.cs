using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    private Color _color;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Setup(Color color)
    {
        _color = color;
        var old = color;
        old.a = 1;
        _spriteRenderer.color = old;
    }
}