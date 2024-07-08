using _Game.Scripts;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class LevelStartSequence : MonoBehaviour
    {
        public ParralaxBackground background;
        public ShipSpeed shipSpeed;
        public bool Active;
        Sequence startSequence;
        public IEnumerator Play()
        {
            background.AdjustSpeed(new Vector2(.3f, 0), 2f);
            startSequence = DOTween.Sequence();
            shipSpeed.AdjustSpeed(new Vector2(.75f, .1f), .1f);
            startSequence.Append(shipSpeed.transform.DOMoveX(2, 2.5f).SetEase(Ease.OutQuad));
            startSequence.AppendInterval(.1f);
            startSequence.AppendCallback(() =>
            {
                shipSpeed.AdjustSpeed(new Vector2(.05f, .1f), 2f);
                background.AdjustSpeed(new Vector2(.05f, 0), 2f);
            }
            );
            startSequence.Append(shipSpeed.transform.DOMoveX(0, 2.5f).SetEase(Ease.InOutQuad));
            yield return startSequence.WaitForCompletion();
            yield break;
        }
    }
}
