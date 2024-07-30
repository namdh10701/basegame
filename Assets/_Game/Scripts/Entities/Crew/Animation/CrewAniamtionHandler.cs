using _Game.Features.Gameplay;
using Spine.Unity;
using UnityEngine;
public enum Direction
{
    Left, Right
}
namespace _Game.Scripts
{
    public class CrewAniamtionHandler : SpineAnimationHandler
    {
        public Crew crew;

        [SerializeField]
        [SpineAnimation]
        string move;

        [SerializeField]
        [SpineAnimation]
        string idle;

        [SerializeField]
        [SpineAnimation]
        string carry;
        [SerializeField]
        [SpineAnimation]
        string fix;
        [SerializeField]
        [SpineAnimation]
        string dropdown;

        [SerializeField] StunFx stunFx;

        private void OnEnable()
        {
            crew.OnStateChanged += OnStateEnter;
        }

        private void OnDisable()
        {
            crew.OnStateChanged -= OnStateEnter;
        }
        public CrewState lastState = CrewState.Idle;

        public string SortingGroup
        {
            set
            {
                skeletonAnimation.GetComponent<MeshRenderer>().sortingLayerName = value;
            }
        }

        public void OnStateEnter(CrewState state)
        {
            switch (state)
            {
                case CrewState.Idle:
                    skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
                    if (lastState == CrewState.Stun)
                    {
                        stunFx.Stop();
                    }
                    break;
                case CrewState.Reparing:
                    skeletonAnimation.AnimationState.SetAnimation(0, fix, true);
                    break;
                case CrewState.Moving:
                    skeletonAnimation.AnimationState.SetAnimation(0, move, true);
                    break;
                case CrewState.Stun:
                    stunFx.Play();
                    skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
                    break;
            }
            lastState = state;
        }

        public void PlayFix()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, fix, true);
        }
        public void PlayMove()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, move, true);
        }
        public void PlayIdle()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, idle, true);

        }
        public void PlayCarry()
        {
            skeletonAnimation.AnimationState.SetAnimation(0, move, true);
            skeletonAnimation.AnimationState.SetAnimation(1, carry, true);
        }

        public void StopCarry()
        {

            skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
            skeletonAnimation.AnimationState.AddEmptyAnimation(1, 0, 0);
        }

        public void Flip(Direction direction)
        {
            if (direction == Direction.Left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {

                transform.localScale = Vector3.one;
            }
        }
    }
}
