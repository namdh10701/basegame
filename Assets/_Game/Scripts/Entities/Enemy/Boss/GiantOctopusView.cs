using _Game.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class GiantOctopusView : MonoBehaviour
    {
        GiantOctopus giantOctopus;
        OctopusState lastState;
        [SerializeField] BodyPartView bodyView;
        [SerializeField] PartView[] partViews;

        [SerializeField] UpperPartView[] upperViews;
        [SerializeField] LowerPartView[] lowerViews;
        [SerializeField] SpawnPartView[] spawnViews;
        public Action OnEntryCompleted;
        [SerializeField] CameraShake cameraShake;
        public void Initialize(GiantOctopus giantOctopus)
        {
            this.giantOctopus = giantOctopus;
            giantOctopus.OnStateEntered += OnStateEntered;
            bodyView.gameObject.SetActive(false);
            foreach (var view in partViews)
            {
                view.gameObject.SetActive(false);
            }
        }

        private void OnStateEntered(OctopusState state)
        {
            if (lastState == state)
            {
                return;
            }

            switch (state)
            {
                case OctopusState.Entry:
                    StartCoroutine(EntryCoroutine());
                    break;
                case OctopusState.State1:

                    break;
                case OctopusState.State2:
                    break;
            }


        }

        private IEnumerator EntryCoroutine()
        {
            foreach (PartView view in spawnViews)
            {
                view.PlayHide();
            }
            yield return new WaitForSeconds(2);
            cameraShake.Shake(5, new Vector3(0.1f, .1f, .1f));
            bodyView.PlayEntry();
            yield return new WaitForSeconds(6.5f);
            cameraShake.Shake(3, new Vector3(0.17f, .17f, .17f));
            bodyView.PlayShake();
            CameraController.Instance.LerpSize(CameraSize.GiantOctopusBoss);
            yield return new WaitForSeconds(.5f);
            for (int i = 0; i < partViews.Length; i++)
            {
                partViews[i].PlayEntry();
                yield return new WaitForSeconds(.25f);
            }
            yield return new WaitForSeconds(1);

            foreach (PartView view in lowerViews)
            {
                view.PlayHide();
            }
            foreach (PartView view in upperViews)
            {
                view.PlayHide();
            }
            bodyView.StopShake();
            yield return new WaitForSeconds(2);
            OnEntryCompleted?.Invoke();
        }

    }
}