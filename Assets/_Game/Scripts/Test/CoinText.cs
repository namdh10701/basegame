using TMPro;
using UnityEngine;
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