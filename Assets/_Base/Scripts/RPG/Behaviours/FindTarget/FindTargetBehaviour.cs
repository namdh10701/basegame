using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget
{
    [AddComponentMenu("RPG/Brain/[Brain] FindTargetBehaviour")]
    // [RequireComponent(typeof(Collider2D))]
    public class FindTargetBehaviour : MonoBehaviour
    {
        // [field:SerializeField]
        // public Collider2D Collider2D { get; set; }

        [field: SerializeField]
        public FindTargetStrategy Strategy { get; set; }

        [field: SerializeField]
        public List<Entity> Targets { get; private set; } = new();

        [field: SerializeField]
        public List<Entity> MostTargets { get; private set; } = new();

        public ObjectCollisionDetector ObjectCollisionDetector;

        private void Awake()
        {
            ObjectCollisionDetector.OnObjectCollisionEnter += OnObjectCollisionEnter;
            ObjectCollisionDetector.OnObjectCollisionExit += OnObjectCollisionExit;
        }

        private void OnDestroy()
        {
            ObjectCollisionDetector.OnObjectCollisionEnter -= OnObjectCollisionEnter;
            ObjectCollisionDetector.OnObjectCollisionExit -= OnObjectCollisionExit;
        }

        private void OnObjectCollisionEnter(GameObject obj)
        {
            var entity = obj.GetComponent<EntityProvider>();

            if (entity == null)
            {
                return;
            }

            if (!Strategy.TryGetTargetEntity(entity.Entity.gameObject, out var target))
            {
                return;
            }

            Targets.Add(target);
        }

        private void OnObjectCollisionExit(GameObject obj)
        {
            var entity = obj.GetComponent<EntityProvider>();

            if (entity == null)
            {
                return;
            }
            if (!Strategy.TryGetTargetEntity(entity.Entity.gameObject, out var target))
            {
                return;
            }
            Targets.Remove(target);
        }

        public void Disable()
        {
            ObjectCollisionDetector.gameObject.SetActive(false);
            Targets.Clear();
            MostTargets.Clear();
        }
        public void Enable()
        {
            ObjectCollisionDetector.gameObject.SetActive(true);
        }

        void Update()
        {
            MostTargets = Strategy.FindTheMostTargets(Targets);
        }

    }
}
