using _Base.Scripts.EventSystem;
using _Game.Scripts;
using DG.Tweening;
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
    [SerializeField] Color takeDamageColor;
    [SerializeField] SpriteRenderer brokenSprite;
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
    Sequence blinkSequence;
    public void Blink()
    {
        if (blinkSequence != null)
        {
            blinkSequence.Kill();
        }

        blinkSequence = DOTween.Sequence();
        spriteRenderer.color = takeDamageColor;
        blinkSequence.Append(spriteRenderer.DOFade(0, .2f));
        blinkSequence.OnComplete(() => blinkSequence = null);
    }

    public void OnBroken()
    {
        brokenSprite.gameObject.SetActive(true);
    }

    public void OnFixed()
    {
        brokenSprite.gameObject.SetActive(false);
    }
}
