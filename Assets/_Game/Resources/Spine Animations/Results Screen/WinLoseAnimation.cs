using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class WinLoseAnimation : MonoBehaviour
    {
        public SkeletonGraphic sg;
        [SpineAnimation] public string winBegin;
        [SpineAnimation] public string winLoop;


        [SpineAnimation] public string loseBegin;
        [SpineAnimation] public string loseLoop;
        public void SetStateIsWin(bool isWin)
        {
            if (isWin)
            {
                sg.Skeleton.SetSkin("win");
                sg.Skeleton.SetToSetupPose();
                sg.AnimationState.SetAnimation(0, winBegin, false);
                sg.AnimationState.AddAnimation(0, winLoop, true, 0);
            }
            else
            {
                sg.Skeleton.SetSkin("loose");
                sg.Skeleton.SetToSetupPose();
                sg.AnimationState.SetAnimation(0, loseBegin, false);
                sg.AnimationState.AddAnimation(0, loseLoop, true, 0);
            }
        }
    }
}