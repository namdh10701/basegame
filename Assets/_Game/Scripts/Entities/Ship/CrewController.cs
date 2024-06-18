
using _Base.Scripts.Utils.Extensions;
using _Game.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour
{
    public CrewJobData CrewJobData;
    public bool HasPendingJob => CrewJobData.PendingJobs.Count > 0;
    public List<Crew> crews = new List<Crew>();
    private void Awake()
    {
        CrewJobData.OnNewJobAdded += OnNewJobAdded;
    }
    public void AddCrew(Crew crew)
    {
        crews.Add(crew);
        crew.crewController = this;
    }
    void OnNewJobAdded(CrewJob crewJob)
    {
        Debug.Log("NEW JOB ADDED");
        Crew crew = GetMostSuitableCrewForJob(crewJob);
        if (crew != null)
        {
            AssignJob(crew, crewJob);
        }
    }

    public void RegisterForNewJob(Crew crew)
    {
        Debug.Log("REGISTER FOR NEW JOB " + crew.name);
        CrewJob job = CrewJobData.GetHighestPiorityJob();
        AssignJob(crew, job);
    }


    Crew GetMostSuitableCrewForJob(CrewJob crewJob)
    {
        Crew ret = GetFreeCrew();
        if (ret == null)
        {
            ret = GetCrewWithLowerJobPiority(crewJob);
            Debug.Log("crew lo" + ret);
        }
        return ret;
    }

    Crew GetFreeCrew()
    {
        List<Crew> freeCrews = new List<Crew>();

        foreach (Crew crew in crews)
        {
            if (crew.ActionHandler.CurrentAction is not CrewJob || crew.ActionHandler.CurrentAction == null)
            {
                Debug.Log("FOUND FREE CREW " + crew.name + crew.ActionHandler.CurrentAction);
                Debug.Log("DETAILS " + " " + (crew.ActionHandler.CurrentAction is not CrewJob) + " " + (crew.ActionHandler.CurrentAction == null));
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
            if (crew.ActionHandler.CurrentAction is CrewJob job)
            {
                if (job.Piority < crewJob.Piority)
                {
                    return crew;
                }
            }
        }
        return null;
    }


    public void AssignJob(Crew crew, CrewJob crewJob)
    {
        Debug.Log("ASSIGNED TO " + crew.name);
        CrewJobData.PendingJobs.Remove(crewJob);
        crewJob.crew = crew;
        crew.ActionHandler.Act(crewJob);
    }

}
