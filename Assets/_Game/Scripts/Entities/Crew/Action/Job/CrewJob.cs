using _Game.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Entities;
using UnityEngine;

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
    public Action<CrewJob> OnJobCompleted;
    public Action<CrewJob> OnJobInterupted;
    public CrewJobAction BuildCrewAction(Crew crew)
    {
        IEnumerator executeCoroutine = Execute(crew);
        IEnumerator interuptCoroutine = Interupt(crew);
        CrewJobAction crewJobAction = new CrewJobAction(this, executeCoroutine, interuptCoroutine);
        return crewJobAction;
    }
    public abstract IEnumerator Execute(Crew crew);
    public abstract IEnumerator Interupt(Crew crew);
}

public class FixCellJob : CrewJob
{
    public Cell cell;
    public FixCellJob(Cell cell)
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
        OnJobInterupted.Invoke(this);
        yield break;
    }
}

public class ReloadCannonJob : CrewJob
{
    public Cannon cannon;
    public Bullet bullet;
    public ReloadCannonJob(Cannon cannon, Bullet bullet)
    {
        Piority = 4;
        WorkLocation = bullet.GetComponent<IWorkLocation>();
        this.cannon = cannon;
        this.bullet = bullet;
    }
    public override IEnumerator Execute(Crew crew)
    {
        crew.Animation.PlayMove();
        List<Cell> cells = new List<Cell>();
        foreach (WorkingSlot workingSlot in WorkLocation.WorkingSlots)
        {
            if (workingSlot.State == WorkingSlotState.Available)
            {
                cells.Add(workingSlot.cell);
            }
        }
        //TODO WorkingSlot
        Cell cellToReachBullet = GridHelper.GetClosetCellToPoint(cells, crew.transform.position);
        Debug.Log(cells.Count);
        WorkingSlot workingOnSlot = null;
        foreach (WorkingSlot workingSlot in WorkLocation.WorkingSlots)
        {
            if (workingSlot.cell == cellToReachBullet)
            {
                workingOnSlot = workingSlot;
                workingSlot.State = WorkingSlotState.Occupied;
            }
        }
        List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, cellToReachBullet.transform.position);
        yield return crew.CrewMovement.MoveByPath(path);
        yield return new WaitForSeconds(.5f);
        workingOnSlot.State = WorkingSlotState.Available;
        crew.Animation.PlayCarry();
        crew.carryObject.gameObject.SetActive(true);
        crew.carryObject.sprite = bullet.Def.ProjectileImage;
        Cell cellToReachCannon = GridHelper.GetClosetAvailableCellSurroundShape(crew.Ship.ShipSetup.Grids[0].Cells, cannon.OccupyCells, crew.transform.position);
        //TODO WorkingSlot
        List<Vector3> path1 = crew.pathfinder.GetPath(crew.transform.position, cellToReachCannon.transform.position);
        yield return crew.CrewMovement.MoveByPathCarry(path1);
        yield return new WaitForSeconds(.5f);
        cannon.Reloader.Reload(bullet.Projectile);
        crew.carryObject.gameObject.SetActive(false);
        yield break;
    }

    public override IEnumerator Interupt(Crew crew)
    {
        yield break;
    }
}


[Serializable]
public abstract class CrewAction
{
    public Cell OccupyingCell;
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
