using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDmgOverTime : MonoBehaviour
{
    StatModifier statModifer;
    float increaseRatePercent;
    public void Init(Stat targetStat, float increaseRatePercent)
    {
        statModifer = new StatModifier(0, StatModType.PercentAdd);
        targetStat.AddModifier(statModifer);
        this.increaseRatePercent = increaseRatePercent;
    }
    private void Update()
    {
        statModifer.Value += Time.deltaTime * increaseRatePercent;
    }
}
