
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using _Game.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    public CrewJobData CrewJobData;
    public bool HasPendingJob => CrewJobData.ActivateJobs.Count > 0;
    public List<Crew> crews = new List<Crew>();
    private void Awake()
    {
        CrewJobData.OnActivateJobsChanged += OnJobActivate;
    }
    public void AddCrew(Crew crew)
    {
        crews.Add(crew);
        crew.crewController = this;
    }
    void OnJobActivate(CrewJob crewJob)
    {
        Crew crew = GetMostSuitableCrewForJob(crewJob);
        if (crew != null)
        {
            AssignJob(crew, crewJob);
        }
    }

    public void RegisterForNewJob(Crew crew)
    {
        List<CrewJob> highestPiorityJobs = CrewJobData.GetHighestPiorityJobs();
        if (highestPiorityJobs.Count == 0)
        {
            return;
        }
        CrewJob closetJob = GetClosetJobFromPosition(highestPiorityJobs, crew);
        if (closetJob == null)
        {
            return;
        }
        AssignJob(crew, closetJob);
    }

    CrewJob GetClosetJobFromPosition(List<CrewJob> jobs, Crew crew)
    {
        float distance = Mathf.Infinity;
        CrewJob closetJob = jobs[0];
        foreach (CrewJob job in jobs)
        {
            List<Node> workingSlots = job.WorkLocation.WorkingSlots;
            float distanceToJob = 0;
            float minDistance = Mathf.Infinity;
            Node minDistanceWorkingSlot;
            foreach (Node workingSlot in workingSlots)
            {
                distanceToJob = Vector2.Distance(workingSlot.transform.position, crew.transform.position);
                if (distanceToJob < minDistance)
                {
                    minDistance = distanceToJob;
                    minDistanceWorkingSlot = workingSlot;
                }
            }

            if (minDistance < distance)
            {
                distance = minDistance;
                closetJob = job;
            }
        }
        return closetJob;
    }

    Crew GetMostSuitableCrewForJob(CrewJob crewJob)
    {
        Crew ret = GetFreeCrew();

        if (ret == null)
        {
            if (crewJob.Piority == int.MaxValue)
            {
                ret = GetClosetCrewToJob(crewJob);
            }
            else
            {
                ret = GetCrewWithLowerJobPiority(crewJob);
            }
        }
        return ret;
    }

    Crew GetFreeCrew()
    {
        List<Crew> freeCrews = new List<Crew>();

        foreach (Crew crew in crews)
        {
            if (crew.ActionHandler.CurrentAction is not CrewJobAction || crew.ActionHandler.CurrentAction == null)
            {
                Debug.Log("FOUND FREE CREW " + crew.name + crew.ActionHandler.CurrentAction);
                Debug.Log("DETAILS " + " " + (crew.ActionHandler.CurrentAction is not CrewJobAction) + " " + (crew.ActionHandler.CurrentAction == null));
                freeCrews.Add(crew);
            }
        }
        Crew ret = freeCrews.GetRandom();
        return ret;
    }
    Crew GetCrewWithLowerJobPiority(CrewJob crewJob)
    {
        foreach (Crew crew in crews)
        {
            if (crew.ActionHandler.CurrentAction is CrewJobAction action)
            {
                if (action.CrewJob.Piority < crewJob.Piority)
                {
                    return crew;
                }
            }
        }
        return null;
    }

    Crew GetClosetCrewToJob(CrewJob crewJob)
    {
        Crew ret;
        return DistanceHelper.GetClosetToPosition(crews.ToArray(), crewJob.WorkLocation.WorkingSlots[0].transform.position).GetComponent<Crew>();
    }

    public void AssignJob(Crew crew, CrewJob crewJob)
    {
        Debug.Log("ASSIGNED TO "+ crew.name);
        CrewJobAction action = crewJob.BuildCrewAction(crew);
        crew.ActionHandler.Act(action);
    }

}
