using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Entities;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    public class TargetInRange : MonoBehaviour
    {
        [SerializeField] ObjectCollisionDetector collisionDetector;
        [SerializeField] public Ship TargetShip;
        public bool IsMet;
        private void OnEnable()
        {
            collisionDetector.OnObjectCollisionEnter += CollisionDetector_OnObjectCollisionEnter;
            collisionDetector.OnObjectCollisionExit += CollisionDetector_OnObjectCollisionExit;
        }

        private void CollisionDetector_OnObjectCollisionExit(GameObject obj)
        {
            var entityBody = obj.GetComponent<EntityProvider>();

            if (entityBody == null)
            {
                return;
            }
            if (entityBody.Entity.gameObject == TargetShip.gameObject)
            {
                IsMet = false;
            }
        }

        private void CollisionDetector_OnObjectCollisionEnter(GameObject obj)
        {
            var entityBody = obj.GetComponent<EntityProvider>();

            if (entityBody == null)
            {
                return;
            }
            if (entityBody.Entity.gameObject == TargetShip.gameObject)
            {
                IsMet = true;
            }
        }
    }
}
