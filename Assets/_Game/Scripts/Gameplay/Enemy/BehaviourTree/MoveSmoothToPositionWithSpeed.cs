using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MBT;
using TMPro;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Move Smooth To Position With Speed")]
    [AddComponentMenu("")]
    public class MoveSmoothToPositionWithSpeed : Leaf
    {
        public Rigidbody2D body;
        public float movementSpeed = 5f; // Speed of movement
        [SerializeField] Vector2Reference destination;
        private Vector2 startPos;
        public float deviation;
        float journeyLength;
        float elapsedTime = 0;
        public override void OnEnter()
        {
            base.OnEnter();
            startPos = body.position;
            elapsedTime = 0;
            journeyLength = Vector2.Distance(startPos, destination.Value);
        }

        public override NodeResult Execute()
        {
            float distanceCovered = (elapsedTime * movementSpeed);

            float fractionOfJourney = distanceCovered / journeyLength;
            fractionOfJourney = Mathf.Clamp01(fractionOfJourney);

            Vector2 pos = Vector3.Lerp(startPos, destination.Value, fractionOfJourney);
            body.MovePosition(pos);
            elapsedTime += Time.deltaTime;

            if (Vector2.Distance(body.position, destination.Value) > deviation)
            {
                return NodeResult.running;
            }
            else
            {
                body.MovePosition(destination.Value);
                return NodeResult.success;
            }
        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }


}
