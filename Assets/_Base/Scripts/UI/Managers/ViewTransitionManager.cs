using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public enum Transition
{
    CrossFade, None
}
public class ViewTransitionManager : AbstractSingleton<ViewTransitionManager>
{
    [SerializeField] Image image;
    [SerializeField] float duration;
    Color transparent = new Color(0, 0, 0, 0);

    private void Start()
    {
        image.color = transparent;
    }
    public void TransitCrossFade(Action activeView)
    {
        image.raycastTarget = true;
        image.DOFade(1, duration / 2).OnComplete
            (() =>
                {
                    activeView();
                    image.DOFade(0, duration / 2).OnComplete(() =>
                    image.raycastTarget = false);
                }
            );
    }
}