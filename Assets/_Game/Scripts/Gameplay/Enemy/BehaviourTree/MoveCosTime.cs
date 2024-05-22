using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using System;
using Unity.Mathematics;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Move Cos Time")]
    [AddComponentMenu("")]
    public class MoveCosTime : Leaf
    {
        [SerializeField] private Rigidbody2D body;

        [SerializeField] private DeviableVector2 direction;
        [SerializeField] private DeviableFloat targetHighSpeed;
        [SerializeField] private DeviableFloat targetLowSpeed;

        [SerializeField] private DeviableFloat accelerateDuration;
        [SerializeField] private DeviableFloat decelerateDuration;

        [SerializeField] private DeviableFloat lockMaxSpeedDuration;

        private Vector2 currentVel;
        private float currentSpeed;
        private float elapsedTime = 0;
        private float startSpeed;
        private Vector2 addDirection;

        public override void OnEnter()
        {
            base.OnEnter();

            accelerateDuration.RefreshValue();
            decelerateDuration.RefreshValue();
            targetHighSpeed.RefreshValue();
            targetLowSpeed.RefreshValue();
            direction.RefreshValue();
            lockMaxSpeedDuration.RefreshValue();
            currentVel = Vector2.zero;
            elapsedTime = 0;
            startSpeed = body.velocity.magnitude;
        }

        public override NodeResult Execute()
        {

            elapsedTime += Time.deltaTime;
            if (elapsedTime < accelerateDuration.Value)
            {
                currentSpeed = Mathf.Lerp(startSpeed, targetHighSpeed.Value, elapsedTime / accelerateDuration.Value);
            }
            else if (elapsedTime < accelerateDuration.Value + lockMaxSpeedDuration.Value)
            {
                currentSpeed = targetHighSpeed.Value;
            }
            else if (elapsedTime < accelerateDuration.Value + lockMaxSpeedDuration.Value + decelerateDuration.Value)
            {
                currentSpeed = Mathf.Lerp(targetHighSpeed.Value, targetLowSpeed.Value,
                    (elapsedTime - accelerateDuration.Value - lockMaxSpeedDuration.Value) / decelerateDuration.Value);
            }
            else
            {
                return NodeResult.success;
            }

            currentVel = direction.Value * currentSpeed;
            body.velocity = currentVel;

            return NodeResult.running;
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    [Serializable]
    public struct DeviableFloat
    {
        public float BaseValue;
        public float Deviation;
        float value;
        public float Value => value;
        public DeviableFloat(float baseValue, float deviation)
        {
            BaseValue = baseValue;
            Deviation = deviation;
            value = BaseValue;
        }
        public void RefreshValue()
        {
            value = BaseValue + UnityEngine.Random.Range(-Deviation, Deviation);
        }
    }

    [Serializable]
    public struct DeviableVector2
    {
        public Vector2 BaseValue;
        public Vector2 Deviation;

        Vector2 value;
        public Vector2 Value => value;
        public void RefreshValue()
        {
            value = BaseValue + new Vector2(UnityEngine.Random.Range(-Deviation.x, Deviation.x), UnityEngine.Random.Range(-Deviation.y, Deviation.y));
        }
    }
}
