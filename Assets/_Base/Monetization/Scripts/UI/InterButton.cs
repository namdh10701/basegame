using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InterButton : Button
{
    [SerializeField] bool _isShowingInter;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!_isShowingInter)
        {
            base.OnPointerClick(eventData);
            return;
        }
        StartCoroutine(TestCoroutine(eventData));
        /* AdsController.Instance.ShowInter(() =>
         {
             base.OnPointerClick(eventData);
         });*/
    }

    IEnumerator TestCoroutine(PointerEventData eventData)
    {
        yield return new WaitForSecondsRealtime(2);
        base.OnPointerClick(eventData);
    }
}
