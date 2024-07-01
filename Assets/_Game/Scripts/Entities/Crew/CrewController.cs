
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using _Game.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    }

    void OnJobActivate(CrewJob crewJob)
    {
        if (crewJob.Piority == int.MaxValue)
        {
            Crew crew = GetMostSuitableCrewForJob(crewJob);
            if (crew != null)
            {
                lastAssignCrew = crew;
                AssignJob(crew, crewJob);
            }
        }
        else
        {
            List<Crew> freeCrews = GetFreeCrews();
            if (freeCrews.Count > 0)
            {
                Crew crew = GetClosetCrewToJob(crewJob, freeCrews);
                if (crew != null)
                {
                    AssignJob(crew, crewJob);
                }
            }
        }
    }

    public void RegisterForNewJob(Crew crew)
    {
        List<CrewJob> highestPiorityJobs = CrewJobData.GetHighestPiorityActiveJobs();
        if (highestPiorityJobs.Count == 0)
        {
            return;
        }
        CrewJob closetJob = GetClosetJobFromCrew(highestPiorityJobs, crew);
        if (closetJob == null)
        {
            return;
        }
        AssignJob(crew, closetJob);
    }




    CrewJob GetClosetJobFromCrew(List<CrewJob> jobs, Crew crew)
    {
        float closestDistance = Mathf.Infinity;
        CrewJob closestJob = null;
        foreach (CrewJob job in jobs)
        {
            Debug.Log(job.Status + " " + job.Name);
            List<Node> workingSlots = job.WorkLocation.WorkingSlots;

            foreach (Node workingSlot in workingSlots)
            {
                float distanceToJob = Vector2.Distance(workingSlot.transform.position, crew.transform.position);

                if (distanceToJob < closestDistance)
                {
                    closestDistance = distanceToJob;
                    closestJob = job;
                }
            }
        }
        return closestJob;
    }

    Crew GetMostSuitableCrewForJob(CrewJob crewJob)
    {
        foreach (Crew crew in crews)
        {
            if (crew.CrewAction.CurrentAction != null && crew.CrewAction.CurrentAction is CrewJobAction crewJobaction)
            {
                if (crewJobaction.CrewJob == crewJob)
                {
                    return crew;
                }
            }
        }
        Debug.Log("GEt Suitable crew job");
        if (crewJob.WorkLocation.WorkingSlots.Count == 0)
        {
            Debug.Log("PKPK");
            return null;
        }
        List<Crew> ret = GetFreeCrews();
        Crew closestFreeCrew = GetClosetCrewToJob(crewJob, ret);
        if (closestFreeCrew != null)
        {
            Debug.Log("HERE 3");
            return closestFreeCrew;
        }
        else
        {
            Debug.Log("HERE 6");
            List<Crew> lowerPriorityCrews = GetCrewsWithLowerJobPriority(crewJob);
            if (lowerPriorityCrews.Count == 0)
            {
                Debug.Log("HERE 4");
                return lastAssignCrew;

            }
            else
            {
                Crew closetCrewWithLowerPriority = GetClosetCrewToJob(crewJob, lowerPriorityCrews);
                return closetCrewWithLowerPriority;
            }
        }
    }
    Crew lastAssignCrew;


    List<Crew> GetFreeCrews()
    {
        List<Crew> freeCrews = new List<Crew>();

        foreach (Crew crew in crews)
        {
            if (crew.CrewAction.CurrentAction is not CrewJobAction || crew.CrewAction.CurrentAction == null)
            {
                freeCrews.Add(crew);
            }
        }
        return freeCrews;
    }
    Crew GetCrewWithLowerJobPiority(CrewJob crewJob)
    {
        foreach (Crew crew in crews)
        {
            if (crew.CrewAction.CurrentAction is CrewJobAction action)
            {
                if (action.CrewJob.Piority < crewJob.Piority)
                {
                    return crew;
                }
            }
        }
        return null;
    }

    Crew GetClosetCrewToJob(CrewJob crewJob, List<Crew> crews)
    {
        Crew closestCrew = null;
        float closestDistance = Mathf.Infinity;

        foreach (Crew crew in crews)
        {
            foreach (Node workingSlot in crewJob.WorkLocation.WorkingSlots)
            {
                float distance = Vector2.Distance(crew.transform.position, workingSlot.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCrew = crew;
                }
            }
        }
        return closestCrew;
    }

    List<Crew> GetCrewsWithLowerJobPriority(CrewJob crewjob)
    {
        List<Crew> ret = new List<Crew>();
        foreach (Crew crew in crews)
        {
            if (crew.CrewAction.CurrentAction is CrewJobAction crewJobAction)
            {
                if (crewJobAction.CrewJob.Piority < crewjob.Piority)
                {
                    ret.Add(crew);
                    continue;
                }
            }
        }
        return ret;
    }

    public void AssignJob(Crew crew, CrewJob crewJob)
    {
        Debug.Log("ASSIGNED TO " + crew.name);
        CrewJobAction action = crewJob.BuildCrewAction(crew);
        crew.CrewAction.DoJob(action);
    }

    public void ActivateCrews()
    {
        foreach (Crew crew in crews)
        {
            crew.CrewAction.Activate();
        }
    }
}
