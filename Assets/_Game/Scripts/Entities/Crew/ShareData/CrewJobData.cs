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
    public Action<CrewJob> OnActivateJobsChanged;

    public ShipSetup ShipSetup;

    public Dictionary<Cell, FixCellJob> FixCellJobDic = new Dictionary<Cell, FixCellJob>();

    public Dictionary<Cannon, ReloadCannonJob> ReloadCannonJobsDic = new Dictionary<Cannon, ReloadCannonJob>();

    //case 1 crew: click cannon -> reload cannon default bullet,
    //             click bullet -> deactivate the reload cannonjob -> activate reload cannon with clicked bullet job
    //case 2 crew: 

    int crewNumber = 2;
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
            ReloadCannonJob reloadCannonJob = new ReloadCannonJob(cannon);
            if (ReloadCannonJobsDic.ContainsKey(cannon))
            {
                ReloadCannonJobsDic.Add(cannon, reloadCannonJob);
            }
            reloadCannonJob.OnJobCompleted += OnJobCompleted;
            reloadCannonJob.OnJobInterupted += OnJobInterupted;

        }
        Debug.Log(ReloadCannonJobsDic.Count + " Reload Cannon Job Count");
        Debug.Log(FixCellJobDic.Count + " Fix Cell Job Count");
    }

    private void Awake()
    {
        GlobalEvent<Cannon, Bullet>.Register("ReloadManual", ActivateReloadCannonJobManual);
        GlobalEvent<Cannon, Bullet>.Register("Reload", ActivateReloadCannonJob);
        GlobalEvent<Cell>.Register("Fix", ActivateFixCellJob);
    }
    void ActivateReloadCannonJobManual(Cannon cannon, Bullet bullet)
    {
        ReloadCannonJob reloadCannonJob = ReloadCannonJobsDic[cannon];

        if (!ActivateJobs.Contains(reloadCannonJob))
        {
            reloadCannonJob.Piority = int.MaxValue;
            reloadCannonJob.bullet = bullet;
            ActivateJobs.Add(reloadCannonJob);
            OnActivateJobsChanged?.Invoke(reloadCannonJob);
        }
        else
        {
            reloadCannonJob.Piority = int.MaxValue;
            reloadCannonJob.bullet = bullet;
            OnActivateJobsChanged.Invoke(reloadCannonJob);
        }
    }
    void ActivateReloadCannonJob(Cannon cannon, Bullet bullet)
    {
        ReloadCannonJob reloadCannonJob = ReloadCannonJobsDic[cannon];
        if (!ActivateJobs.Contains(reloadCannonJob))
        {
            reloadCannonJob.bullet = bullet;
            ActivateJobs.Add(reloadCannonJob);
            OnActivateJobsChanged?.Invoke(reloadCannonJob);
        }
    }

    void ActivateFixCellJob(Cell cell)
    {
        FixCellJob fixCellJob = FixCellJobDic[cell];
        if (!ActivateJobs.Contains(fixCellJob))
        {
            ActivateJobs.Add(fixCellJob);
            OnActivateJobsChanged?.Invoke(fixCellJob);
        }
    }

    void OnJobInterupted(CrewJob crewJob)
    {
        if (ActivateJobs.Contains(crewJob))
        {
            ActivateJobs.Remove(crewJob);
            crewJob.Piority = crewJob.DefaultPiority;
            crewJob.IsJobActivated = false;
            crewJob.Status = JobStatus.Free;
        }
    }

    void OnJobCompleted(CrewJob crewJob)
    {
        crewJob.IsJobActivated = false;
        crewJob.Piority = crewJob.DefaultPiority;
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
