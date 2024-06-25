using _Base.Scripts.RPG.Behaviours.FindTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.FollowTarget
{
    [AddComponentMenu("RPG/Brain/[Brain] FollowTargetBehaviour")]
    public class FollowTargetBehaviour : MonoBehaviour
    {
        [field: SerializeField]
        public FollowTargetStrategy Strategy { get; set; }

        [field: SerializeField]
        public FindTargetBehaviour FindTargetBehaviour { get; set; }

        [field: SerializeField]
        public bool IsCaughtUp { get; private set; }

        [field: SerializeField]
        public Transform Target { get; private set; }

        void Update()
        {
            IsCaughtUp = Strategy.Follow(FindTargetBehaviour);
        }

    }
}
