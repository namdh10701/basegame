using _Base.Scripts.UI;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CannonProgressBar : ProgressBarDisplay
    {
        public Sprite onFever;
        public Sprite normal;
        public void OnFeverEnter()
        {
            Progress.sprite = onFever;
        }

        public void OnFeverExit()
        {
            Progress.sprite = normal;
        }
    }
}
