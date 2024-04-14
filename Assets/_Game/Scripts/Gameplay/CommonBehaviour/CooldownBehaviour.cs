using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBehaviour : MonoBehaviour
{
    private float cooldownTime;
    private Coroutine cooldownCoroutine;
    private Action onCooldownCompleted;
    public bool IsInCooldown => cooldownCoroutine != null;
    public void StartCooldown()
    {
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        cooldownCoroutine = null;
        onCooldownCompleted?.Invoke();
    }

    public void OnCooldownCompleted(Action onCooldownCompleted)
    {
        this.onCooldownCompleted = onCooldownCompleted;
    }

    public void SetCooldownTime(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
    }

}
