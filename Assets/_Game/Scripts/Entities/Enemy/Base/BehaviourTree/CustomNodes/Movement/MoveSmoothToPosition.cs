using UnityEngine;
using MBT;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Move Smooth To Position")]
    [AddComponentMenu("")]
    public class MoveSmoothToPosition : Leaf
    {
        public Rigidbody2D body;
        [SerializeField] private float duration;
        [SerializeField] private float elapsedTime;
        [SerializeField] Vector2Reference destination;
        private Vector2 startPos;
        public override void OnEnter()
        {
            base.OnEnter();
            startPos = body.position;
            elapsedTime = 0;
        }

        public override NodeResult Execute()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > duration)
            {
                elapsedTime = duration;
            }

            Vector2 newPos = Vector2.Lerp(startPos, destination.Value, elapsedTime / duration);
            body.MovePosition(newPos);
            if (elapsedTime < duration)
            {
                return NodeResult.running;
            }
            else
            {
                body.MovePosition(destination.Value);
                elapsedTime = duration;
                return NodeResult.success;
            }
        }
        public override void OnExit()
        {
            base.OnExit();
        }
    }


}
