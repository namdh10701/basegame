using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class BattleResultAnim : MonoBehaviour
    {
        [SerializeField] SkeletonGraphic skeletonGraphic;
        [SerializeField] bool isWinning;
        private void Start()
        {
            SetStateWinning(isWinning);
        }
        public void SetStateWinning(bool isWinning)
        {
            if (isWinning)
            {
                skeletonGraphic.Skeleton.SetSkin("win");
                skeletonGraphic.AnimationState.SetAnimation(0, "WIN_BEGIN", false);
                skeletonGraphic.AnimationState.SetAnimation(0, "WIN_LOOP", false);
            }
            else
            {
                skeletonGraphic.Skeleton.SetSkin("loose");
                skeletonGraphic.AnimationState.SetAnimation(0, "LOOSE_BEGIN", false);
                skeletonGraphic.AnimationState.SetAnimation(0, "LOOSE_LOOP", false);
            }
        }
    }
}