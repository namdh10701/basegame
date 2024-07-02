using MBT;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Move Beizer")]
    [AddComponentMenu("")]
    public class MoveBeizer : Leaf
    {
        public Vector2Reference destination;
        private Vector2 startPoint;
        private Vector2 controlPoint;
        public Rigidbody2D body;
        public float controlPointDistance;
        public float controlPointOffset;
        public float t;
        public float duration;
        public float elapsedTime = 0;
        public override void OnEnter()
        {
            base.OnEnter();
            startPoint = body.position;
            t = 0;
            elapsedTime = 0;
            CalculateControlPoint();
        }

        public override NodeResult Execute()
        {

            elapsedTime += Time.deltaTime;
            // Increment parameter based on time
            if (elapsedTime >= duration)
            {
                elapsedTime = duration;
                t = 1;
            }
            t = elapsedTime / duration;
            // Calculate position along the Bezier curve
            Vector3 position = CalculateBezierPoint(startPoint, controlPoint, destination.Value, t);

            // Move the Rigidbody2D to the calculated position
            body.MovePosition(position);
            Debug.Log(Vector2.Distance(body.position, destination.Value));
            return Vector2.Distance(body.position, destination.Value) > 0.1 ? NodeResult.running : NodeResult.success;
        }


        void CalculateControlPoint()
        {
            controlPoint = (startPoint + destination.Value) / 2f;
            controlPoint += Vector2.up * controlPointOffset; // Adjust height if needed
            t = 0f; // Reset parameter
        }
        private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }
    }
}