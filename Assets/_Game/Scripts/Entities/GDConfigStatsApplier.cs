using _Game.Scripts;
using _Game.Scripts.GD;
using Online;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDConfigStatsApplier : MonoBehaviour
{
    public void LoadStats(IGDConfigStatsTarget target)
    {
        string id = GetComponent<IGDConfigStatsTarget>().Id;

        IStatsBearer statsBearer = GetComponent<IStatsBearer>();
        Stats targetStats = statsBearer.Stats;
        if (PlayfabManager.Instance != null)
        {

        }
        statsBearer.ApplyStats();
    }
}
