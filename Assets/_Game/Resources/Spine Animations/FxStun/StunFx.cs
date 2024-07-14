using DG.Tweening;
using Spine;
using Spine.Unity;
using UnityEngine;

public class StunFx : MonoBehaviour
{
    Tween fade;
    [SerializeField] SkeletonAnimation skeletonAnimation;
    Skeleton skeleton;
    [SpineAnimation][SerializeField] string stun;

    public void Play()
    {
        /*if (fade != null)
        {
            fade.Kill();
        }*/
        gameObject.SetActive(true);
        //skeletonAnimation.skeleton.SetColor(Color.white);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
       /* if (fade != null || !gameObject.activeSelf)
        {
            return;
        }
        fade = DOTween.ToAlpha(() => skeleton.GetColor(), x =>
        {
            skeleton.SetColor(x);

        }, 0f, 0.25f).OnComplete(() =>
        {
            fade = null;
            gameObject.SetActive(false);
        });*/
    }
    private void OnDestroy()
    {
        if (fade != null)
        {
            fade.Kill();
        }
    }
}
