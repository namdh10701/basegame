using _Base.Scripts.RPG.Behaviours.FollowTarget;
using UnityEngine;

namespace _Base.Scripts.RPG.Behaviours.AimTarget
{
    [AddComponentMenu("RPG/Brain/[Brain] AimTargetBehaviour")]
    public class AimTargetBehaviour : MonoBehaviour
    {
        [field:SerializeField]
        public AimTargetStrategy Strategy { get; set; }

        [field:SerializeField]
        public FollowTargetBehaviour FollowTargetBehaviour { get; set; }
        
        [field:SerializeField]
        public bool IsReadyToAttack { get; private set; }
        
        [field:SerializeField]
        public Vector3 LockedPosition { get; private set; }

        void Update()
        {
            if (FollowTargetBehaviour.IsCaughtUp)
            {
                IsReadyToAttack = Strategy.Aim(FollowTargetBehaviour);
                if (FollowTargetBehaviour.FindTargetBehaviour.MostTarget != null)
                {
                    LockedPosition = FollowTargetBehaviour.FindTargetBehaviour.MostTarget.transform.position;
                }
            }
            else
            {
                Strategy.Reset();
                IsReadyToAttack = false;
                LockedPosition = Vector3.zero;
            }
        }

    }
}
