using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeTrigger : MonoBehaviour
{
    private Transform target;
    private Action onTargetInRangeCallback;
    private Action onTargetOutRangeCallback;
    private bool isInRange;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public void AddListener(Action onInRangeCallback, Action onOutRangeCallback)
    {
        this.onTargetInRangeCallback = onInRangeCallback;
        this.onTargetOutRangeCallback = onOutRangeCallback;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.Equals(target))
        {
            if (!isInRange)
            {
                isInRange = true;
                onTargetInRangeCallback?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.Equals(target))
        {
            if (isInRange)
            {
                isInRange = false;
                onTargetOutRangeCallback?.Invoke();
            }
        }
    }
}
