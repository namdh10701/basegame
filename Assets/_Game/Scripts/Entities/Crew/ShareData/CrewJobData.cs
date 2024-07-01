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

    public List<CrewJob> GetHighestPiorityActiveJobs()
    {
        List<CrewJob> highestPriorityJobs = new List<CrewJob>();
        int highestPriority = -1;

        foreach (CrewJob crewJob in ActivateJobs)
        {
            if (crewJob.Status != JobStatus.WorkingOn && crewJob.Piority > highestPriority)
            {
                highestPriority = crewJob.Piority;
            }
        }

        foreach (CrewJob crewJob in ActivateJobs)
        {
            if (crewJob.Status != JobStatus.WorkingOn && crewJob.Piority == highestPriority)
            {
                highestPriorityJobs.Add(crewJob);
            }
        }

        return highestPriorityJobs;
    }
    //case 1 crew: click cannon -> reload cannon default bullet,
    //             click bullet -> deactivate the reload cannonjob -> activate reload cannon with clicked bullet job
    //case 2 crew: 

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
            if (!ReloadCannonJobsDic.ContainsKey(cannon))
            {
                ReloadCannonJobsDic.Add(cannon, reloadCannonJob);
            }
            reloadCannonJob.OnJobCompleted += OnJobCompleted;
            reloadCannonJob.OnJobInterupted += OnJobInterupted;

        }

        Debug.Log(FixCellJobDic.Count + " FIX CELL");
        Debug.Log(ReloadCannonJobsDic.Count + " RELOAD CANNON");
    }

    private void Awake()
    {
        // Cannon, Bullet, Piority
        GlobalEvent<Cannon, Bullet, int>.Register("Reload", ActivateReloadCannonJob);
        // Cell, Piority
        GlobalEvent<Cell, int>.Register("Fix", ActivateFixCellJob);
    }

    private void OnDestroy()
    {
        GlobalEvent<Cannon, Bullet, int>.Unregister("Reload", ActivateReloadCannonJob);
     
     
        GlobalEvent<Cell, int>.Unregister("Fix", ActivateFixCellJob);
    }

    void ActivateReloadCannonJob(Cannon cannon, Bullet bullet, int piority = 0)
    {
        ReloadCannonJob reloadCannonJob = ReloadCannonJobsDic[cannon];
        if (piority == 0)
        {
            reloadCannonJob.Piority = reloadCannonJob.DefaultPiority;
        }
        else
        {
            reloadCannonJob.Piority = piority;
        }
        if (reloadCannonJob.Piority != int.MaxValue)
        {
            if (!ActivateJobs.Contains(reloadCannonJob))
            {
                ActivateJobs.Add(reloadCannonJob);
            }
        }

        if (reloadCannonJob.bullet == bullet && reloadCannonJob.Status == JobStatus.WorkingOn)
        {
            Debug.Log(reloadCannonJob.Status);
            return;
        }
        else if (reloadCannonJob.bullet == null || reloadCannonJob.bullet != bullet)
        {
            Debug.Log("asign new bullet to job ");
            reloadCannonJob.AssignBullet(bullet);
        }
        OnActivateJobsChanged.Invoke(reloadCannonJob);

    }
    void ActivateFixCellJob(Cell cell, int piority)
    {
        FixCellJob fixCellJob = FixCellJobDic[cell];
        if (piority == 0)
        {
            fixCellJob.Piority = fixCellJob.DefaultPiority;
        }
        else
        {
            fixCellJob.Piority = piority;
        }
        if (!ActivateJobs.Contains(fixCellJob))
        {
            ActivateJobs.Add(fixCellJob);
        }
        if (fixCellJob.Status == JobStatus.WorkingOn)
        {
            return;
        }
        OnActivateJobsChanged?.Invoke(fixCellJob);

    }

    void OnJobInterupted(CrewJob crewJob)
    {
        if (ActivateJobs.Contains(crewJob))
        {
            crewJob.Piority = crewJob.DefaultPiority;
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
}
