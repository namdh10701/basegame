using _Game.Features.Battle;
using Spine.Unity;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Features.Gameplay
{
    public class FeverView : MonoBehaviour
    {
        public SkeletonGraphic SkeletonGraphic;

        [SpineAnimation] public string appear;
        [SpineAnimation] public string idle;
        [SpineAnimation] public string feverIdle;
        [SpineAnimation] public string fevering;
        [SpineAnimation] public string feverToNormal;
        [SpineAnimation] public string normalToFever;
        public StarWing State1Wing;
        public StarWing State2Wing;
        public StarWing State3Wing;


        public FeverModel FeverModel;
        public FeverState lastState;

        public FeverBtn feverBtn;


        public void Init(FeverModel feverModel)
        {
            this.FeverModel = feverModel;
            OnStateEnter(feverModel.CurrentState);
            lastState = feverModel.CurrentState;
            this.FeverModel.OnStateChanged += OnFeverStateChanged;
            SkeletonGraphic.AnimationState.AddAnimation(0, appear, false, 0);
            SkeletonGraphic.AnimationState.AddAnimation(0, idle, true, 0);
        }

        public void ClearState()
        {
            this.FeverModel.OnStateChanged += OnFeverStateChanged;
            State1Wing.HideCompletely();
            State2Wing.HideCompletely();
            State3Wing.HideCompletely();
        }

        public void OnFeverStateChanged(FeverState currentState)
        {
            if (lastState != currentState)
            {
                OnStateEnter(currentState);
                lastState = currentState;
            }
        }

        void OnStateEnter(FeverState state)
        {
            switch (state)
            {
                case FeverState.State0:
                    if (lastState == FeverState.State1)
                    {
                        State1Wing.Hide();
                    }
                    else if (lastState == FeverState.Unleashing)
                    {
                        feverBtn.Hide();
                        // Handle transition from Unleashing to State0
                        SkeletonGraphic.AnimationState.AddAnimation(0, feverToNormal, false, 0);
                        SkeletonGraphic.AnimationState.AddAnimation(0, idle, true, 0);
                    }
                    break;
                case FeverState.State1:
                    if (lastState == FeverState.State2)
                    {
                        State2Wing.Hide();
                    }
                    else if (lastState == FeverState.State0)
                    {
                        State1Wing.Show();
                    }
                    break;
                case FeverState.State2:
                    if (lastState == FeverState.State3)
                    {
                        State3Wing.Hide();
                    }
                    else if (lastState == FeverState.State1)
                    {
                        State2Wing.Show();
                    }
                    break;
                case FeverState.State3:
                    if (lastState == FeverState.State4)
                    {
                    }
                    else if (lastState == FeverState.State2)
                    {
                        State3Wing.Show();
                    }
                    break;
                case FeverState.State4:
                    if (lastState == FeverState.State3)
                    {
                        SkeletonGraphic.AnimationState.AddAnimation(0, normalToFever, true, 0);
                        SkeletonGraphic.AnimationState.AddAnimation(0, feverIdle, true, 0);
                        feverBtn.Show();
                    }
                    break;
                case FeverState.Unleashing:
                    SkeletonGraphic.AnimationState.AddAnimation(0, fevering, true, 0);
                    break;
            }
        }
    }
}