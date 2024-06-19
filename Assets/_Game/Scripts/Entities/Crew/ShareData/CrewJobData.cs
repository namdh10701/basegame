using _Base.Scripts.EventSystem;
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewJobData : MonoBehaviour
{
    public List<CrewJob> ActivateJobs = new List<CrewJob>();
    public Action<CrewJob> OnJobActivate;

    public ShipSetup ShipSetup;

    public Dictionary<Cell, FixCellJob> FixCellJobDic = new Dictionary<Cell, FixCellJob>();
    public Dictionary<KeyValuePair<Cannon, Bullet>, ReloadCannonJob> ReloadCannonJobsDic = new Dictionary<KeyValuePair<Cannon, Bullet>, ReloadCannonJob>();


    public void Initialize()
    {
        foreach (Cell cell in ShipSetup.AllCells)
        {
            FixCellJob fixCellJob = new FixCellJob(cell);
            FixCellJobDic.Add(cell, fixCellJob);
            fixCellJob.OnJobCompleted += OnJobCompleted;
            fixCellJob.OnJobInterupted += OnJobInterupted;
        }
        foreach (Cannon cannon in ShipSetup.Cannons)
        {
            foreach (Bullet bullet in ShipSetup.bullets)
            {
                ReloadCannonJob reloadCannonJob = new ReloadCannonJob(cannon, bullet);
                ReloadCannonJobsDic.Add(new KeyValuePair<Cannon, Bullet>(cannon, bullet), reloadCannonJob);

                reloadCannonJob.OnJobCompleted += OnJobCompleted;
                reloadCannonJob.OnJobInterupted += OnJobInterupted;
            }
        }
        Debug.Log(ReloadCannonJobsDic.Count + " Reload Cannon Job Count");
        Debug.Log(FixCellJobDic.Count + " Fix Cell Job Count");
    }

    private void Awake()
    {
        GlobalEvent<Cannon, Bullet>.Register("ReloadManual", ActivateReloadCannonJobManual);
        GlobalEvent<Cannon, Bullet>.Register("Reload", ActivateReloadCannonJob);
        GlobalEvent<Cell>.Register("Broken", ActivateFixCellJob);
    }
    void ActivateReloadCannonJobManual(Cannon cannon, Bullet bullet)
    {
        ReloadCannonJob reloadCannonJob = ReloadCannonJobsDic[new KeyValuePair<Cannon, Bullet>(cannon, bullet)];
        if (!ActivateJobs.Contains(reloadCannonJob))
        {
            reloadCannonJob.Piority = int.MaxValue;
            ActivateJobs.Add(reloadCannonJob);
            OnJobActivate?.Invoke(reloadCannonJob);
        }
    }
    void ActivateReloadCannonJob(Cannon cannon, Bullet bullet)
    {
        ReloadCannonJob reloadCannonJob = ReloadCannonJobsDic[new KeyValuePair<Cannon, Bullet>(cannon, bullet)];
        if (!ActivateJobs.Contains(reloadCannonJob))
        {
            ActivateJobs.Add(reloadCannonJob);
            OnJobActivate?.Invoke(reloadCannonJob);
        }
    }

    void ActivateFixCellJob(Cell cell)
    {
        FixCellJob fixCellJob = FixCellJobDic[cell];
        if (!ActivateJobs.Contains(fixCellJob))
        {
            ActivateJobs.Add(fixCellJob);
            OnJobActivate?.Invoke(fixCellJob);
        }
    }

    void OnJobInterupted(CrewJob crewJob)
    {

    }

    void OnJobCompleted(CrewJob crewJob)
    {
        crewJob.IsJobActivated = false;
        if (ActivateJobs.Contains(crewJob))
        {
            ActivateJobs.Remove(crewJob);
        }
    }

    public List<CrewJob> GetHighestPiorityJobs()
    {
        List<CrewJob> ret = new List<CrewJob>();
        int piority = -1;
        foreach (CrewJob crewJob in ActivateJobs)
        {
            if (crewJob.Status == JobStatus.Free)
            {
                if (crewJob.Piority > piority)
                {
                    piority = crewJob.Piority;
                }
            }
        }

        foreach (CrewJob crewJob in ActivateJobs)
        {
            if (crewJob.Piority == piority)
            {
                ret.Add(crewJob);
            }
        }
        return ret;
    }
}
