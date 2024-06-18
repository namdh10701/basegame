using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobStatus
{
    Free, WorkingOn, Completed
}
[Serializable]
public abstract class CrewJob : CrewAction
{
    public Crew crew;
    public int Piority;
    public JobStatus Status = JobStatus.Free;
}
public class FixCellJob : CrewJob
{


    public override IEnumerator Execute()
    {
        Status = JobStatus.WorkingOn;
        yield return new WaitForSeconds(3);
        Status = JobStatus.Completed;
    }

    public override IEnumerator Interupt()
    {
        Status = JobStatus.Free;
        yield break;
    }
}

[Serializable]
public abstract class CrewAction
{
    public Cell OccupyingCell;
    public abstract IEnumerator Execute();
    public abstract IEnumerator Interupt();
}
