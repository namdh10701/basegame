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
    public List<CrewJob> PendingJobs = new List<CrewJob>();
    public Action<CrewJob> OnNewJobAdded;
    private void Awake()
    {
        GlobalEvent<Cannon, Bullet>.Register("Reload", AddReloadCannonJob);
        GlobalEvent<Cell>.Register("Broken", AddFixCellJob);
    }

    void AddReloadCannonJob(Cannon cannon, Bullet bullet)
    {
        ReloadCannonJob reloadCannon = new ReloadCannonJob(cannon, bullet);
        PendingJobs.Add(reloadCannon);
        OnNewJobAdded.Invoke(reloadCannon);
    }

    void AddFixCellJob(Cell cell)
    {
        FixCellJob fixCellJob = new FixCellJob(cell);
        PendingJobs.Add(fixCellJob);
        OnNewJobAdded.Invoke(fixCellJob);
    }

    public List<CrewJob> GetHighestPiorityJobs()
    {
        List<CrewJob> ret = new List<CrewJob>();
        int piority = -1;


        foreach (CrewJob crewJob in PendingJobs)
        {
            if (crewJob.Piority > piority)
            {
                piority = crewJob.Piority;
            }
        }

        foreach (CrewJob crewJob in PendingJobs)
        {
            if (crewJob.Piority == piority)
            {
                ret.Add(crewJob);
            }
        }
        return ret;
    }
}
