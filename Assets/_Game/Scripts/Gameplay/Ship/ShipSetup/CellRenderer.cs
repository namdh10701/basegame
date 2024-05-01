using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HighlightType
{
    Accepted, Denied, Normal 
}
public class CellRenderer : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color appceptedColor;
    [SerializeField] Color deniedColor;
    [SerializeField] Color normalColor;

    public void ToggleHighlight(HighlightType highlightType)
    {
        switch (highlightType)
        {
            case HighlightType.Accepted:
                spriteRenderer.color = appceptedColor;
                break;
            case HighlightType.Denied:

                spriteRenderer.color = deniedColor;
                break;
            case HighlightType.Normal:
                spriteRenderer.color = normalColor;
                break;
        }
    }
}
