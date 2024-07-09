using UnityEngine;
using UnityEngine.UI;

namespace _Base.Scripts.UI
{
    public class ProgressBarDisplay : MonoBehaviour
    {
        public Image Progress;
        public void SetProgress(float amount)
        {
            Progress.fillAmount = amount;
        }
    }
}