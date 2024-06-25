using System.Linq;
using _Base.Scripts.RPG.Behaviours.FollowTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AimTarget
{
    [AddComponentMenu("RPG/Brain/[Brain] AimTargetBehaviour")]
    public class AimTargetBehaviour : MonoBehaviour
    {
        [field: SerializeField]
        public AimTargetStrategy Strategy { get; set; }

        [field: SerializeField]
        public FollowTargetBehaviour FollowTargetBehaviour { get; set; }

        [field: SerializeField]
        public bool IsReadyToAttack { get; private set; }

        [field: SerializeField]
        public Vector3 LockedPosition { get; private set; }
        void Update()
        {
            IsReadyToAttack = Strategy.Aim(FollowTargetBehaviour);
            if (FollowTargetBehaviour.FindTargetBehaviour.MostTargets.Count > 0)
            {
                LockedPosition = FollowTargetBehaviour.FindTargetBehaviour.MostTargets.First().transform.position;
            }
            else
            {
                LockedPosition = Vector2.zero;
            }
        }

    }
}
