using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Stats;
using UnityEngine;

public class IncreaseDmgOverTime : MonoBehaviour
{
    DecreaseHealthEffect dhe;
    float orgAmount;
    float increaseRatePercent;
    float totalIncrease = 0;
    public void Init(DecreaseHealthEffect decreaseHealthEffect, float increaseRatePercent)
    {
        dhe = decreaseHealthEffect;
        orgAmount = decreaseHealthEffect.Amount;
        this.increaseRatePercent = increaseRatePercent;
    }
    private void Update()
    {
        totalIncrease += Time.deltaTime * increaseRatePercent;
        if (dhe != null)
            dhe.Amount = orgAmount + orgAmount * (totalIncrease / 100);
    }
}
