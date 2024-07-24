using _Game.Scripts;
using _Game.Scripts.PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class CrewMovement : MonoBehaviour
    {
        public Crew crew;
        public PathfindingController pathfinder;

        private void Start()
        {
            //pathfinder = FindAnyObjectByType<Ship>().PathfindingController;
        }

        public Vector2 Velocity
        {
            get
            {
                return crew.body.velocity;
            }
            set
            {
                crew.body.velocity = value;
            }
        }
        public IEnumerator MoveTo(Vector3 destination)
        {
            List<Vector3> path = pathfinder.GetPath(crew.transform.position, destination);
            yield return MoveByPath(path);
        }

        public IEnumerator MoveCarry(Vector3 destination)
        {
            List<Vector3> path = pathfinder.GetPath(crew.transform.position, destination);
            yield return MoveByPathCarry(path);
        }

        public IEnumerator MoveByPath(List<Vector3> path)
        {
            if (path == null)
            {
                yield break;
            }
            foreach (Vector3 waypoint in path)
            {
                Vector3 direction = (waypoint - crew.transform.position).normalized;
                if (direction.x > 0)
                {
                    crew.Animation.Flip(Direction.Right);
                }
                else if (direction.x < 0)
                {
                    crew.Animation.Flip(Direction.Left);
                }
                while (Vector3.Distance(crew.transform.position, waypoint) > 0.1f)
                {
                    crew.Animation.Flip(direction.x > 0 ? Direction.Right : Direction.Left);
                    direction = (waypoint - crew.transform.position).normalized;
                    crew.body.velocity = direction * crew.stats.MoveSpeed.Value;
                    yield return new WaitForFixedUpdate();
                }
                crew.body.velocity = Vector3.zero;
            }
            yield break;
        }

        public IEnumerator MoveByPathCarry(List<Vector3> path)
        {
            if (path == null)
            {
                yield break;
            }
            crew.Animation.PlayCarry();
            foreach (Vector3 waypoint in path)
            {
                Vector3 direction = (waypoint - crew.transform.position).normalized;
                if (direction.x > 0)
                {
                    crew.Animation.Flip(Direction.Right);
                }
                else if (direction.x < 0)
                {
                    crew.Animation.Flip(Direction.Left);
                }
                while (Vector3.Distance(crew.transform.position, waypoint) > 0.1f)
                {
                    crew.Animation.Flip(direction.x > 0 ? Direction.Right : Direction.Left);
                    direction = (waypoint - crew.transform.position).normalized;
                    crew.body.velocity = direction * crew.stats.MoveSpeed.Value;
                    yield return new WaitForFixedUpdate();
                }
                crew.body.velocity = Vector3.zero;
            }
            yield break;
        }

        internal bool IsAbleToMoveTo(Node targetNode)
        {
            return pathfinder.GetPath(transform.position, targetNode.transform.position) == null;
        }
    }
}