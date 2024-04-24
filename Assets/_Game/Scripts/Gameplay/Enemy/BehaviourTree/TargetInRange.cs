using _Base.Scripts.RPG;
using _Game.Scripts;
using _Game.Scripts.Gameplay.Ship;
using MBT;
using System.Collections;
using System.Collections.Generic;
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
            if (obj == shipReference.Value)
            {
                isMet = false;
            }
        }

        private void CollisionDetector_OnObjectCollisionEnter(GameObject obj)
        {
            Debug.Log(obj.name);

            Debug.Log(shipReference.Value.gameObject);
            if (obj == shipReference.Value.gameObject)
            {
                Debug.Log("HERE");
                isMet = true;
            }
        }

        public override bool Check()
        {
            return isMet;
        }
    }
}
