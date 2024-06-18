using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum JobStatus
{
    Free, WorkingOn, Interupting, Completed
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
    Cell needFixCell;
    public FixCellJob(Cell cell)
    {
        Piority = 2;
        this.needFixCell = cell;
    }
    public override IEnumerator Execute()
    {
        Status = JobStatus.WorkingOn;
        List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, needFixCell.transform.position);
        yield return crew.CrewMovement.MoveByPath(path);
        yield return new WaitForSeconds(.5f);
        crew.Animation.PlayFix();
        yield return new WaitForSeconds(2f);
        needFixCell.OnFixed();
        Status = JobStatus.Completed;
    }

    public override IEnumerator Interupt()
    {
        Status = JobStatus.Interupting;
        yield return null;
        Status = JobStatus.Free;
    }
}

[Serializable]
public abstract class CrewAction
{
    public Cell OccupyingCell;
    public abstract IEnumerator Execute();
    public abstract IEnumerator Interupt();
}
