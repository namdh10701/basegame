using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget
{
    [AddComponentMenu("RPG/Brain/[Brain] FindTargetBehaviour")]
    [RequireComponent(typeof(CircleCollider2D))]
    public class FindTargetBehaviour : MonoBehaviour
    {
        [field:SerializeField]
        public FindTargetStrategy Strategy { get; set; }
        
        [field:SerializeField]
        public List<Entity> Targets { get; private set; } = new ();
        
        [field:SerializeField]
        [CanBeNull] public Entity MostTarget { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = Strategy.GetTarget(collision.gameObject);

            if (target == null)
            {
                return;
            }

            Targets.Add(target);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var target = Strategy.GetTarget(collision.gameObject);

            if (target == null)
            {
                return;
            }
            Targets.Remove(target);
        }

        void Update()
        {
            MostTarget = Strategy.FindTheMostTarget(this);
        }

    }
}
