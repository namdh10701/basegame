using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetView : MonoBehaviour, IGridItemView
{
    public static Color BrokenColor = new Color(.25f, .25f, .25f, 1);
    public static Color ActiveColor = Color.white;
    public SpriteRenderer spriteRenderer;
    public void HandleActive()
    {
        spriteRenderer.color = ActiveColor;
    }

    public void HandleBroken()
    {
        spriteRenderer.color = BrokenColor;
    }

    public void Init(IGridItem gridItem)
    {

    }

}
