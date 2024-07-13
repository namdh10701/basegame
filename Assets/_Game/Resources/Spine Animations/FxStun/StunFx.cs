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

    private void Start()
    {
        skeleton = skeletonAnimation.skeleton;
    }

    public void Play()
    {
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        fade = DOTween.ToAlpha(() => skeleton.GetColor(), x =>
        {
            skeleton.SetColor(x);

        }, 0f, 0.25f).OnComplete(() =>
        {
            fade = null;
            gameObject.SetActive(false);
        });
    }
    private void OnDisable()
    {
        skeleton.SetColor(Color.white);
    }
    private void OnDestroy()
    {
        if (fade != null)
        {
            fade.Kill();
        }
    }
}
