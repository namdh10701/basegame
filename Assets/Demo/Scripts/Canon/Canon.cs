﻿using System;
using System.Collections;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] public CanonData CanonData;
    [SerializeField] private CanonShoot gunShoot;
    [SerializeField] private RotateBrain rotateBrain;
    [SerializeField] private SightBrain sightBrain;
    [SerializeField] private CooldownBrain cooldownBrain;

    private void Start()
    {
        cooldownBrain.SetCooldownTime(1 / CanonData.AttackSpeed);
    }
    public enum State
    {
        Idle, Rotating, Attacking
    }

    private State currentState = State.Idle;

    void UpdateState()
    {
        switch (currentState)
        {
            case State.Idle:
                if (sightBrain.CurrentTarget != null)
                {
                    currentState = State.Rotating;
                }
                break;
            case State.Rotating:
                if (rotateBrain.IsLookingAtTarget)
                {
                    currentState = State.Attacking;
                }
                if (sightBrain.CurrentTarget == null)
                {
                    currentState = State.Idle;
                }
                break;
            case State.Attacking:
                if (sightBrain.CurrentTarget == null)
                {
                    currentState = State.Idle;
                }
                break;
        }
    }
    private void Update()
    {  
        Debug.Log(Time.deltaTime + "NOR");

        UpdateState();
        sightBrain.FindTarget();
        switch (currentState)
        {
            case State.Idle:
                rotateBrain.ResetRotate();
                break;
            case State.Rotating:
                rotateBrain.Rotate(sightBrain.CurrentTarget.transform);

                if (cooldownBrain.IsResting)
                {
                    cooldownBrain.WarmUp();
                }
                break;
            case State.Attacking:
                rotateBrain.Rotate(sightBrain.CurrentTarget.transform);

                if (cooldownBrain.IsResting)
                {
                    cooldownBrain.WarmUp();
                }

                if (!cooldownBrain.IsInCooldown && !cooldownBrain.IsResting)
                {
                    gunShoot.Shoot(sightBrain.CurrentTarget.transform);
                    cooldownBrain.StartCooldown();
                }
                break;
        }

    }

    // nếu cooldown xong đợi 0.5s mà không gọi cooldown tiếp thì đi vào trạng thái rest, lần sau sẽ cần đợi warm up trước khi bắn phát đầu
}