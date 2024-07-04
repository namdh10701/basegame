using _Base.Scripts.RPG.Stats;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image progress;
    public Cannon cannon;

    private void Start()
    {
        progress = transform.GetChild(0).GetComponent<Image>();
        ((CannonStats)cannon.Stats).HealthPoint.OnValueChanged += HealthPoint_OnValueChanged;
    }

    private void HealthPoint_OnValueChanged(RangedStat stat)
    {
        progress.fillAmount = stat.Value / (stat.MinValue + stat.MaxValue);
    }
    private void OnDestroy()
    {
        ((CannonStats)cannon.Stats).HealthPoint.OnValueChanged -= HealthPoint_OnValueChanged;
    }
}
