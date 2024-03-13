using System;
using System.Collections;
using UnityEngine;

public class AttackBrain : MonoBehaviour
{
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private RotateBrain rotateBrain;
    [SerializeField] private SightBrain sightBrain;
    [SerializeField] private CooldownBrain cooldownBrain;

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
        UpdateState();
        switch (currentState)
        {
            case State.Idle:
                sightBrain.FindTarget();
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
                    attackBehaviour.Attack(sightBrain.CurrentTarget.transform);
                    cooldownBrain.StartCooldown();
                }
                break;
        }
    }

    // nếu cooldown xong đợi 0.5s mà không gọi cooldown tiếp thì đi vào trạng thái rest, lần sau sẽ cần đợi warm up trước khi bắn phát đầu
}