
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Features.Battle
{
    public class FeverSpeedFx : MonoBehaviour
    {
        public Sprite[] sprites;
        public float framesPerSecond = 15f;

        [SerializeField] private Image image;
        [SerializeField] private Image bgImage;
        private float timePerFrame;
        private int currentFrame;
        bool isActive;

        Tween fade;
        Tween fadeBg;
        void Start()
        {
            timePerFrame = 1f / framesPerSecond;
            currentFrame = 0;
        }

        void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.V))
            {
                Activate();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Deactivate();
            }*/

            if (isActive)
            {
                timePerFrame = 1f / framesPerSecond;
                currentFrame = (int)(Time.time / timePerFrame) % sprites.Length;
                image.sprite = sprites[currentFrame];
            }
        }

        public void Activate()
        {
            if (fade != null)
                fade.Kill();
            image.gameObject.SetActive(true);
            bgImage.gameObject.SetActive(true);
            isActive = true;
            fade = image.DOFade(1, .2f).OnComplete(() => fade = null);


            if (fadeBg != null)
                fadeBg.Kill();
            fadeBg = bgImage.DOFade(.5f, .2f).OnComplete(() => fade = null);
        }

        public void Deactivate()
        {
            if (fade != null)
                fade.Kill();

            image.gameObject.SetActive(false);
            fade = image.DOFade(0, .2f).OnComplete(() => fade = null);

            if (fadeBg != null)
                fadeBg.Kill();
            fadeBg = bgImage.DOFade(0f, .2f).OnComplete(() => fade = null);
        }
        private void OnDestroy()
        {
            if (fade != null)
            {
                fade.Kill();
            }
        }
    }
}