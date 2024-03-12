using _Base.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class LoadingView : View
    {
        [SerializeField] Image progressBar;

        public void SetProgress(float amount)
        {
            progressBar.fillAmount = amount;
        }
    }
}
