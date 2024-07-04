using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridItemAliveStatsListener : AliveStatsListener
{
    public float BrokenThreshold;
    public UnityEvent OnBelowBrokenThreshold;
    IAliveStats aliveStats;
    protected override void Awake()
    {
        base.Awake();
        aliveStats = (IAliveStats)GetComponent<Entity>().Stats;
        aliveStats.HealthPoint.OnValueChanged += BrokenCheck;
    }

    private void BrokenCheck(_Base.Scripts.RPG.Stats.RangedStat hp)
    {
        if (hp.Value <= 0)
        {
            OnBelowBrokenThreshold.Invoke();
        }

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        aliveStats.HealthPoint.OnValueChanged -= BrokenCheck;
    }
}
