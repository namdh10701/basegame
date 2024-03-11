using System;
using System.Collections;
using UnityEngine;
public class LerpManager : ObservedSingleton<LerpManager>
{
    [HideInInspector] public float CurrentValue;
    public float LerpDuration;
    private Coroutine lerpCoroutine;

    public void OnValueChanged(int newValue)
    {
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }

        lerpCoroutine = StartCoroutine(LerpCoroutine(newValue));
    }

    IEnumerator LerpCoroutine(int newValue)
    {
        float elapsedTime = 0f;
        float startValue = CurrentValue;
        while (elapsedTime < LerpDuration)
        {
            CurrentValue = (int)Mathf.Lerp(startValue, newValue, elapsedTime / LerpDuration);
            elapsedTime += Time.unscaledDeltaTime;
            Notify();
            yield return null;
        }
        CurrentValue = newValue;
        Notify();
        lerpCoroutine = null;
    }
}