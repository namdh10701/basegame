using _Game.Features.Battle;
using Spine.Unity;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Features.Gameplay
{
    public class FeverView : MonoBehaviour
    {
        public TextMeshProUGUI feverText;
        public Image feverProgress;

        public SkeletonGraphic SkeletonGraphic;

        [SpineAnimation] public string appear;
        [SpineAnimation] public string idle;
        [SpineAnimation] public string feverIdle;
        [SpineAnimation] public string fevering;
        [SpineAnimation] public string feverToNormal;
        [SpineAnimation] public string normalToFever;
        public GameObject Orb;
        public StarWing State1Wing;
        public StarWing State2Wing;
        public StarWing State3Wing;


        public FeverModel FeverModel;
        public FeverState lastState;

        public FeverBtn feverBtn;


        public void Init(FeverModel feverModel)
        {
            this.FeverModel = feverModel;

            SkeletonGraphic.AnimationState.AddAnimation(0, appear, false, 0);
            SkeletonGraphic.AnimationState.AddAnimation(0, idle, true, 0);
            OnStateEnter(feverModel.CurrentState);
            lastState = feverModel.CurrentState;
            feverModel.OnStatChanged += FeverStat_OnValueChanged;
            this.FeverModel.OnStateChanged += OnFeverStateChanged;
        }

        private void FeverStat_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat stat)
        {
            if (FeverModel.CurrentState != FeverState.Unleashing)
            {
                feverText.text = $"{stat.Value.ToString()} / {stat.MaxValue.ToString()}";
            }
        }

        public void ClearState()
        {
            lastState = FeverState.State0;
            State1Wing.gameObject.SetActive(false);
            State2Wing.gameObject.SetActive(false);
            State3Wing.gameObject.SetActive(false);
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
                        Orb.gameObject.SetActive(false);
                        State1Wing.Hide();
                        State2Wing.Hide();
                        State3Wing.Hide();
                    }
                    else if (lastState == FeverState.Unleashing)
                    {
                        feverBtn.Hide();
                        State1Wing.Show();
                        State2Wing.Show();
                        State3Wing.Show();

                        State1Wing.Hide();
                        State2Wing.Hide();
                        State3Wing.Hide();
                        // Handle transition from Unleashing to State0
                        SkeletonGraphic.AnimationState.SetAnimation(0, feverToNormal, false);
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
                        Orb.gameObject.SetActive(true);
                        State1Wing.Show();
                    }
                    break;
                case FeverState.State2:
                    if (lastState == FeverState.State3)
                    {
                        State3Wing.Hide();
                    }
                    else if (lastState == FeverState.State1 || lastState == FeverState.State0)
                    {
                        Orb.gameObject.SetActive(true);
                        State1Wing.Show();
                        State2Wing.Show();
                    }
                    break;
                case FeverState.State3:
                    if (lastState == FeverState.State4)
                    {
                    }
                    else if (lastState == FeverState.State2 || lastState == FeverState.State0 || lastState == FeverState.State1)
                    {
                        Orb.gameObject.SetActive(true);
                        State1Wing.Show();
                        State2Wing.Show();
                        State3Wing.Show();
                    }
                    break;
                case FeverState.State4:
                    if (lastState == FeverState.State3 || lastState == FeverState.State0 || lastState == FeverState.State1 || lastState == FeverState.State2)
                    {
                        State1Wing.HideCompletely();
                        State2Wing.HideCompletely();
                        State3Wing.HideCompletely();
                        Orb.gameObject.SetActive(false);
                        SkeletonGraphic.AnimationState.SetAnimation(0, normalToFever, true);
                        SkeletonGraphic.AnimationState.AddAnimation(0, feverIdle, true, 0);
                        feverBtn.Show();
                    }
                    break;
                case FeverState.Unleashing:
                    SkeletonGraphic.AnimationState.SetAnimation(0, fevering, true);
                    StartCoroutine(UnleashCoroutine());
                    break;
            }
        }
        IEnumerator UnleashCoroutine()
        {
            float elapsedTime = 0;
            float progress = 0;
            float duration = 10;
            while (progress < 1)
            {
                progress = elapsedTime / duration;
                elapsedTime += Time.deltaTime;
                feverProgress.fillAmount = Mathf.Lerp(0, 1, progress);
                yield return null;
            }
        }
    }
}