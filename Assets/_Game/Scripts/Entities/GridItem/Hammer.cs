using DG.Tweening;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class Hammer : MonoBehaviour
    {
        Sequence sequence;
        [SerializeField] Vector3 startRotation;
        [SerializeField] Vector3 endRotation;
        public void Play()
        {
            if (sequence != null)
            {
                sequence.Kill();
            }
            Debug.Log("PLAY");
            sequence = DOTween.Sequence(sequence);
            transform.rotation = Quaternion.Euler(startRotation);
            sequence.Append(transform.DOLocalRotate(endRotation, .25f));
            sequence.Append(transform.DOLocalRotate(startRotation, .25f));
            sequence.SetLoops(-1);
            sequence.Play();
        }

        public void Stop()
        {
            Debug.Log("Stop");
            if (sequence != null)
            {
                sequence.Kill();
                sequence = null;
                transform.rotation = Quaternion.Euler(startRotation);
            }
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            if (sequence != null)
            {
                sequence.Kill();
            }
        }
    }
}