using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Features.Battle
{
    public class FeverSpeedFx : MonoBehaviour
    {
        public Sprite[] sprites;
        public float framesPerSecond = 15f;

        [SerializeReference] private Image image;
        private float timePerFrame;
        private int currentFrame;
        bool isActive;

        Tween fade;
        void Start()
        {
            timePerFrame = 1f / framesPerSecond;
            currentFrame = 0;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Activate();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Deactivate();
            }

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
            gameObject.SetActive(true);
            isActive = true;
            fade = image.DOFade(1, .2f).OnComplete(() => fade = null);
        }

        public void Deactivate()
        {
            if (fade != null)
                fade.Kill();
            gameObject.SetActive(false);
            fade = image.DOFade(0, .2f).OnComplete(() => fade = null);
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