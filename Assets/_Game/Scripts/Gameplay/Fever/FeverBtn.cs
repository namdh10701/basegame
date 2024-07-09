using _Base.Scripts.EventSystem;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBtn : MonoBehaviour
{
    Tween tween;

    public void Show()
    {
        gameObject.SetActive(true);
        if (tween != null)
        {
            tween.Kill();
        }
        transform.localScale = new Vector3(1, 0, 1);
        tween = transform.DOScaleY(1, .5f).OnComplete(() => tween = null);
    }

    public void Hide()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        tween = transform.DOScaleY(0, .5f).OnComplete(() => { tween = null; gameObject.SetActive(false); });
    }

    private void OnDestroy()
    {
        if (tween != null)
        {
            tween.Kill();
        }
    }

    public void OnClick()
    {
        GlobalEvent.Send("UseFever");
    }
}
