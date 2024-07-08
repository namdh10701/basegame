using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarWing : MonoBehaviour
{
    Tween tween;
    public SkeletonGraphic glow;
    public Sprite disableSprite;
    public Sprite enableSprite;
    public void Show()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        glow.AnimationState.SetAnimation(0, "fx_active_set", false);
        tween = transform.DOScale(1, .25f).OnComplete(() => tween = null);
    }

    public void Hide()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        glow.AnimationState.SetAnimation(0, "fx_deactive_set", false);
        tween = transform.DOScale(0, .25f).OnComplete(() => tween = null);
    }
}
