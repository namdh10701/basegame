using _Base.Scripts.RPG.Entities;
using _Base.Scripts.RPGCommon.Entities;
using UnityEngine;
using UnityEngine.Events;

public class AliveStatsListener : MonoBehaviour
{
    public UnityEvent OnReachZero = new UnityEvent();
    public UnityEvent OnReachFull = new UnityEvent();
    public UnityEvent OnDecreased = new UnityEvent();
    public UnityEvent OnIncreased = new UnityEvent();
    public IAliveStats aliveStats;
    float lastValue;
    protected virtual void Awake()
    {
        aliveStats = (IAliveStats)GetComponent<IStatsBearer>().Stats;
        aliveStats.HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
        lastValue = aliveStats.HealthPoint.Value;
    }

    private void HealthPoint_OnValueChanged(_Base.Scripts.RPG.Stats.RangedStat hp)
    {
        if (hp.Value <= hp.MinValue)
        {
            OnReachZero.Invoke();
        }
        if (hp.IsFull)
        {
            OnReachFull.Invoke();
        }
        if (hp.Value < lastValue)
        {
            OnDecreased.Invoke();
        }
        if (hp.Value > lastValue)
        {
            OnIncreased.Invoke();
        }
        lastValue = hp.Value;
    }

    protected virtual void OnDestroy()
    {
        aliveStats.HealthPoint.OnValueChanged -= HealthPoint_OnValueChanged;
    }
}
