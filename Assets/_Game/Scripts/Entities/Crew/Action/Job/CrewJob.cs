using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Entities;
using UnityEngine;
using System.Linq;

public enum JobStatus
{
    Free, WorkingOn, Interupting, Completed
}
[Serializable]
public abstract class CrewJob
{
    public int DefaultPiority;
    public int Piority;
    public JobStatus Status = JobStatus.Free;
    public IWorkLocation WorkLocation;
    public bool IsJobActivated;
    public Action<CrewJob> OnJobCompleted;
    public Action<CrewJob> OnJobInterupted;
    public CrewJob()
    {
        IsJobActivated = false;
        Status = JobStatus.Free;
    }
    public CrewJobAction BuildCrewAction(Crew crew)
    {
        Status = JobStatus.WorkingOn;
        IEnumerator executeCoroutine = DoExecute(crew);
        IEnumerator interuptCoroutine = DoInterupt(crew);
        CrewJobAction crewJobAction = new CrewJobAction(this, executeCoroutine, interuptCoroutine);
        return crewJobAction;
    }

    public IEnumerator DoExecute(Crew crew)
    {
        yield return Execute(crew);
        OnJobCompleted.Invoke(this);
        Status = JobStatus.Completed;
    }
    public IEnumerator DoInterupt(Crew crew)
    {
        OnJobInterupted.Invoke(this);
        Status = JobStatus.Interupting;
        yield return Interupt(crew);
        Status = JobStatus.Free;
    }

    public abstract IEnumerator Execute(Crew crew);
    public abstract IEnumerator Interupt(Crew crew);
}
