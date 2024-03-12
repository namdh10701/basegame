using _Base.Scripts.Patterns.Observer;
using _Base.Scripts.Utils;
using TMPro;

namespace _Game.Scripts.Test
{
    public class CoinText : Observer
    {
        TextMeshProUGUI cointext;

        private void Awake()
        {
            observedSubject = CurrencyManager.Instance.CoinTextLerpManager;
            cointext = GetComponent<TextMeshProUGUI>();
        }
        public override void OnNotified(ObservedSubject observedSubject)
        {
            cointext.text = ((int)((LerpManager)observedSubject).CurrentValue).ToString();
        }
    }
}