using _Base.Scripts.RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image progress;

    private void Start()
    {
        progress = transform.GetChild(0).GetComponent<Image>();
    }

    public void UpdateProgress(RangedStat stat)
    {
        progress.fillAmount = stat.Value / (stat.MinValue + stat.MaxValue);
    }
}
