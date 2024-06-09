using _Base.Scripts.RPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class EvasionBuffArea : MonoBehaviour
    {
        public ObjectCollisionDetector collisionDetector;

        private void OnEnable()
        {
            collisionDetector.OnObjectCollisionEnter += CollisionDetector_OnObjectCollisionEnter;
            collisionDetector.OnObjectCollisionExit += CollisionDetector_OnObjectCollisionExit;
        }

        private void OnDisable()
        {
            collisionDetector.OnObjectCollisionEnter -= CollisionDetector_OnObjectCollisionEnter;
            collisionDetector.OnObjectCollisionExit -= CollisionDetector_OnObjectCollisionExit;
        }

        private void CollisionDetector_OnObjectCollisionExit(GameObject obj)
        {
            //TODO: MODIFY EVASION HERE
        }

        private void CollisionDetector_OnObjectCollisionEnter(GameObject obj)
        {
            //TODO: MODIFY EVASION HERE
        }
    }
}