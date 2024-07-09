using _Game.Scripts;
using _Game.Scripts.GD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDConfigStatsApplier : MonoBehaviour
{
    public void LoadStats(IGDConfigStatsTarget target)
    {
        string id = GetComponent<IGDConfigStatsTarget>().Id;

        GDConfig gdConfig = target.GDConfig;
        StatsTemplate statsTemplate = target.StatsTemplate;

        IStatsBearer statsBearer = GetComponent<IStatsBearer>();
        Stats targetStats = statsBearer.Stats;
        if (GDConfigLoader.Instance != null)
        {
            GDConfig config = GDConfigLoader.Instance.GetConfig(id, gdConfig);
            if (config != null)
            {
                config.ApplyGDConfig(targetStats);
            }
            else
            {
                statsTemplate.ApplyConfig(targetStats);
            }
        }
        else
        {
            statsTemplate.ApplyConfig(targetStats);
        }

        statsBearer.ApplyStats();
    }
}
