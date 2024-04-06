using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FindTarget
{
    [AddComponentMenu("RPG/Brain/[Brain] FindTargetBehaviour")]
    [RequireComponent(typeof(Collider2D))]
    public class FindTargetBehaviour : MonoBehaviour
    {
        // [field:SerializeField]
        // public Collider2D Collider2D { get; set; }
            
        [field:SerializeField]
        public FindTargetStrategy Strategy { get; set; }
        
        [field:SerializeField]
        public List<Entity> Targets { get; private set; } = new ();

        [field: SerializeField] 
        public List<Entity> MostTargets { get; private set; } = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!Strategy.TryGetTargetEntity(collision.gameObject, out var target))
            {
                return;
            }

            Targets.Add(target);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!Strategy.TryGetTargetEntity(collision.gameObject, out var target))
            {
                return;
            }
            Targets.Remove(target);
        }

        void Update()
        {
            MostTargets = Strategy.FindTheMostTargets(Targets);
        }

    }
}
