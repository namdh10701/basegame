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
        IEnumerator executeCoroutine = DoExecute(crew);
        IEnumerator interuptCoroutine = DoInterupt(crew);
        CrewJobAction crewJobAction = new CrewJobAction(this, executeCoroutine, interuptCoroutine);
        return crewJobAction;
    }

    public IEnumerator DoExecute(Crew crew)
    {
        Status = JobStatus.WorkingOn;
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

public class FixCellJob : CrewJob
{
    public Cell cell;
    public FixCellJob(Cell cell) : base()
    {
        Piority = 3;
        WorkLocation = cell.GetComponent<IWorkLocation>();
        this.cell = cell;
    }
    public override IEnumerator Execute(Crew crew)
    {
        List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, cell.transform.position);
        yield return crew.CrewMovement.MoveByPath(path);
        crew.Animation.PlayFix();
        yield return new WaitForSeconds(3);
        cell.stats.HealthPoint.StatValue.BaseValue = cell.stats.HealthPoint.MaxValue;
        yield break;
    }

    public override IEnumerator Interupt(Crew crew)
    {
        yield break;
    }
}

public class ReloadCannonJob : CrewJob
{
    public Cannon cannon;
    public Bullet bullet;
    public ReloadCannonJob(Cannon cannon, Bullet bullet) : base()
    {
        Piority = 4;
        WorkLocation = bullet.GetComponent<IWorkLocation>();
        this.cannon = cannon;
        this.bullet = bullet;
    }
    public override IEnumerator Execute(Crew crew)
    {
        crew.Animation.PlayMove();
        List<Cell> availableCells = WorkLocation.WorkingSlots
            .Where(slot => slot.State == WorkingSlotState.Available)
            .Select(slot => slot.cell)
            .ToList();

        Cell cellToReachBullet = GridHelper.GetClosetCellToPoint(availableCells, crew.transform.position);
        WorkingSlot workingOnSlot = WorkLocation.WorkingSlots
            .FirstOrDefault(slot => slot.cell == cellToReachBullet);

        workingOnSlot.State = WorkingSlotState.Occupied;
        
        yield return crew.CrewMovement.MoveTo(cellToReachBullet.transform.position);
        yield return new WaitForSeconds(0.5f);
        workingOnSlot.State = WorkingSlotState.Available;
        crew.Carry(bullet);

        List<Cell> avaialbleSlots = cannon.GetComponent<IWorkLocation>().WorkingSlots
            .Where(slot => slot.State == WorkingSlotState.Available)
            .Select(slot => slot.cell)
            .ToList();
        Cell cellToReachCannon = GridHelper.GetClosetCellToPoint(avaialbleSlots, crew.transform.position);

        yield return crew.CrewMovement.MoveTo(cellToReachCannon.transform.position);
        yield return new WaitForSeconds(0.5f);
        cannon.Reloader.Reload(bullet.Projectile);
        crew.StopCarry();
    }

    public override IEnumerator Interupt(Crew crew)
    {
        yield break;
    }
}


[Serializable]
public abstract class CrewAction
{
    public IEnumerator Execute;
    public IEnumerator Interupt;
}

public class CrewJobAction : CrewAction
{
    public CrewJob CrewJob;
    public CrewJobAction(CrewJob crewJob, IEnumerator execute, IEnumerator interupt)
    {
        this.CrewJob = crewJob;
        this.Execute = execute;
        this.Interupt = interupt;
    }
}
