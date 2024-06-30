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
    public string Name;
    public int DefaultPiority;
    public int Piority;
    public JobStatus Status = JobStatus.Free;
    public IWorkLocation WorkLocation;
    public bool IsJobActivated;
    public Action<CrewJob> OnJobCompleted;
    public Action<CrewJob> OnJobInterupted;
    Crew crew;
    public CrewJob()
    {
        IsJobActivated = false;
        Status = JobStatus.Free;
    }
    public CrewJobAction BuildCrewAction(Crew crew)
    {
        this.crew = crew;
        IEnumerator executeCoroutine = DoExecute(crew);
        CrewJobAction crewJobAction = new CrewJobAction(this, executeCoroutine);
        return crewJobAction;
    }

    public IEnumerator DoExecute(Crew crew)
    {
        Status = JobStatus.WorkingOn;
        yield return Execute(crew);
        OnJobCompleted.Invoke(this);
        Status = JobStatus.Completed;
    }
    public void DoInterupt()
    {
        OnJobInterupted.Invoke(this);
        Status = JobStatus.Interupting;
        Interupt(crew);
        Status = JobStatus.Free;
    }

    public abstract IEnumerator Execute(Crew crew);
    public abstract void Interupt(Crew crew);
}
