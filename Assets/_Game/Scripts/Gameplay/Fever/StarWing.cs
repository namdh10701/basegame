using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarWing : MonoBehaviour
{
    Tween tween;
    public SkeletonGraphic glow;
    public Sprite disableSprite;
    public Sprite enableSprite;
    public Image image;

    public bool IsShowed;
    public void Show()
    {
        image.sprite = enableSprite;
        gameObject.SetActive(true);
        if (IsShowed)
        {
            glow.AnimationState.SetAnimation(0, "fx_active_set", false);
            return;
        }
        if (tween != null)
        {
            tween.Kill();
        }
        IsShowed = true;
        glow.AnimationState.SetAnimation(0, "fx_active_set", false);
        tween = transform.DOScale(1, .25f).OnComplete(() => tween = null);
    }

    public void Hide()
    {
        image.sprite = disableSprite;
        glow.AnimationState.SetAnimation(0, "fx_deactive_set", false);

    }

    internal void HideCompletely()
    {
        IsShowed = false;
        image.sprite = disableSprite;
        glow.AnimationState.SetAnimation(0, "fx_deactive_set", false);
        tween = transform.DOScale(0, .25f).OnComplete(() => tween = null);
    }
}
