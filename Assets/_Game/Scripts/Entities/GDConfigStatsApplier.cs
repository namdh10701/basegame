using _Game.Scripts;
using _Game.Scripts.GD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDConfigStatsApplier : MonoBehaviour
{
    public StatsTemplate statsTemplate;
    public GDConfig gdConfig;

    public GDConfig GDConfig { get => gdConfig; }
    public StatsTemplate StatTemplate { get => statsTemplate; }

    public Stats targetStats;
    private void Start()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        Debug.Log("Load stats");
        string id = GetComponent<IGDConfigStatsTarget>().Id;
        IStatsBearer statsBearer = GetComponent<IStatsBearer>();
        targetStats = statsBearer.Stats;

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
