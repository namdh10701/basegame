using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class LevelStartSequence : MonoBehaviour
    {
        public ParralaxBackground background;
        public ShipSpeed shipSpeed;
        public bool Active;
        Sequence startSequence;
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.V))
            {
                Activate();
            }
        }

        void Activate()
        {
            startSequence = DOTween.Sequence();
            shipSpeed.AdjustSpeed(new Vector2(.75f, .1f), .1f);
            startSequence.Append(shipSpeed.transform.DOMoveX(2, 2.5f).SetEase(Ease.OutQuad));
            startSequence.AppendInterval(.1f);
            startSequence.AppendCallback(() =>
            {
                shipSpeed.AdjustSpeed(new Vector2(.25f, .1f), 2f);
                background.AdjustSpeed(new Vector2(.1f, 0), 2f);
            }
            );
            startSequence.Append(shipSpeed.transform.DOMoveX(0, 2.5f).SetEase(Ease.InOutQuad));

        }
    }
}
