using _Game.Features.Battle;
using Spine.Unity;
using UnityEngine;

namespace _Game.Features.GamePause
{
    public class FeverView : MonoBehaviour
    {
        public SkeletonAnimation SkeletonGraphic;

        [SpineAnimation] public string appear;
        [SpineAnimation] public string idle;

        public GameObject[] AllWings;
        public GameObject[] State1Wings;
        public GameObject[] State2Wings;
        public GameObject[] State3Wings;
        public GameObject[] State4Wings;


        public FeverModel FeverModel;
        public FeverState lastState;
        public void Init(FeverModel feverModel)
        {
            this.FeverModel = feverModel;
            OnStateEnter(feverModel.CurrentState);
            lastState = feverModel.CurrentState;
            this.FeverModel.OnStateChanged += OnFeverStateChanged;
            SkeletonGraphic.AnimationState.SetAnimation(0, appear, false);
            SkeletonGraphic.AnimationState.AddAnimation(0, idle, true, 0);
        }
        public void OnFeverStateChanged(FeverState currentState)
        {
            OnStateEnter(currentState);
            lastState = currentState;
        }

        void OnStateEnter(FeverState state)
        {
            if (state == FeverState.State0 && lastState == FeverState.State1)
            {

            }
            else if (state == FeverState.State0 && lastState == FeverState.Unleashing)
            {

            }

            ToggleStarWing(state);

        }


        public void ToggleStarWing(FeverState state)
        {
            switch (state)
            {
                case FeverState.State0:
                    foreach (GameObject gameObject in AllWings)
                    {
                        gameObject.SetActive(false);
                    }
                    break;
                case FeverState.State1:
                    foreach (GameObject gameObject in AllWings)
                    {
                        gameObject.SetActive(false);
                    }
                    foreach (GameObject gameObject1 in State1Wings)
                    {
                        gameObject1.SetActive(false);
                    }
                    break;
                case FeverState.State2:
                    foreach (GameObject gameObject in AllWings)
                    {
                        gameObject.SetActive(false);
                    }
                    foreach (GameObject gameObject1 in State2Wings)
                    {
                        gameObject1.SetActive(false);
                    }
                    break;
                case FeverState.State3:
                    foreach (GameObject gameObject in AllWings)
                    {
                        gameObject.SetActive(false);
                    }
                    foreach (GameObject gameObject1 in State3Wings)
                    {
                        gameObject1.SetActive(false);
                    }
                    break;
                case FeverState.State4:

                    foreach (GameObject gameObject1 in State4Wings)
                    {
                        gameObject1.SetActive(false);
                    }
                    break;
                case FeverState.Unleashing:

                    break;
            }
        }
    }
}