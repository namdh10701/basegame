using _Base.Scripts.RPG;
using _Base.Scripts.RPG.Entities;
using MBT;
using UnityEngine;

namespace _Game.Scripts.Battle
{
    [MBTNode("My Node/Target In Range")]
    [AddComponentMenu("")]
    public class TargetInRange : MBT.Condition
    {
        [SerializeField] ObjectCollisionDetector collisionDetector;
        [SerializeField] ShipReference shipReference;
        public bool isMet;
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
            if (entityBody.Entity.gameObject == shipReference.Value.gameObject)
            {
                isMet = false;
            }
        }

        private void CollisionDetector_OnObjectCollisionEnter(GameObject obj)
        {
            var entityBody = obj.GetComponent<EntityProvider>();

            if (entityBody == null)
            {
                return;
            }
            if (entityBody.Entity.gameObject == shipReference.Value.gameObject)
            {
                isMet = true;
            }
        }

        public override bool Check()
        {
            return isMet;
        }
    }
}
