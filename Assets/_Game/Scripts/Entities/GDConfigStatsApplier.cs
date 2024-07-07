using _Game.Scripts;
using _Game.Scripts.GD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDConfigStatsApplier : MonoBehaviour
{
    public Stats targetStats;
    private void Start()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        
        IGDConfigStatsTarget target = GetComponent<IGDConfigStatsTarget>();
        string id = GetComponent<IGDConfigStatsTarget>().Id;
        
        GDConfig gdConfig = target.GDConfig;
        StatsTemplate statsTemplate = target.StatsTemplate;

        IStatsBearer statsBearer = GetComponent<IStatsBearer>();
        targetStats = statsBearer.Stats;
        Debug.Log("Load stats" + id + gdConfig);
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
