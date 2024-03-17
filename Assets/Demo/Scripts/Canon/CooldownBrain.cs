using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CooldownBrain : MonoBehaviour
{
    [HideInInspector] private float cooldownTime;
    [SerializeField] float goToRestTime;
    [SerializeField] float warmUpTime;
    Coroutine cooldownCoroutine;
    public bool IsInCooldown => cooldownCoroutine != null;
    [HideInInspector] public bool IsResting;
    bool isWarmingUp;
    bool cancelRest;
    public void StartCooldown()
    {
        Debug.Log("Start Cooldown");
        if (cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        cancelRest = true;
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        cooldownCoroutine = null;
        Debug.Log("Complete Cooldown");
        yield return new WaitForSeconds(goToRestTime);
        Invoke("GoToRest", goToRestTime);
    }
    public void WarmUp()
    {
        if (!isWarmingUp)
        {
            isWarmingUp = true;
            Invoke("GoToReady", warmUpTime);
        }
    }

    void GoToReady()
    {
        IsResting = false;
        isWarmingUp = false;
    }
    void GoToRest()
    {
        if (!cancelRest)
        {
            IsResting = true;
        }
        else
        {
            cancelRest = false;
        }
    }

    public void SetCooldownTime(float cooldownTime)
    {
        this.cooldownTime = cooldownTime;
    }


}