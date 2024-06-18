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
        ReloadCannon reloadCannon = new ReloadCannon(cannon, bullet);
        PendingJobs.Add(reloadCannon);
        OnNewJobAdded.Invoke(reloadCannon);
    }

    void AddFixCellJob(Cell cell)
    {

    }


    public CrewJob GetHighestPiorityJob()
    {
        CrewJob ret = null;
        int piority = -1;
        foreach (CrewJob crewJob in PendingJobs)
        {
            if (crewJob.Piority > piority)
            {
                ret = crewJob;
                piority = crewJob.Piority;
            }
        }
        return ret;
    }
}
