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
    public List<WorkingSlot> WorkingSlot;
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
        this.cell = cell;
        List<Cell> workingCells = GridHelper.GetCellsAroundShape(cell.Grid.Cells, new List<Cell> { cell });
        foreach (Cell workingCell in workingCells)
        {
            WorkingSlot.Add(new WorkingSlot(workingCell));
        }
    }
    public override IEnumerator Execute(Crew crew)
    {

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
    public ReloadCannonJob(Cannon cannon, Bullet bullet)
    {
        this.cannon = cannon;
        this.bullet = bullet;
        List<Cell> workingCells = GridHelper.GetCellsAroundShape(bullet.OccupyCells[0].Grid.Cells, bullet.OccupyCells);
        foreach (Cell workingCell in workingCells)
        {
            WorkingSlot.Add(new WorkingSlot(workingCell));
        }
    }
    public override IEnumerator Execute(Crew crew)
    {
        crew.Animation.PlayMove();
        //TODO WorkingSlot
        Cell cellToReachBullet = GridHelper.GetClosetAvailableCellSurroundShape(crew.Ship.ShipSetup.Grids[0].Cells, bullet.OccupyCells, crew.transform.position);
        List<Vector3> path = crew.pathfinder.GetPath(crew.transform.position, cellToReachBullet.transform.position);
        yield return crew.CrewMovement.MoveByPath(path);
        yield return new WaitForSeconds(.5f);
        crew.Animation.PlayCarry();
        crew.carryObject.gameObject.SetActive(true);
        crew.carryObject.sprite = bullet.Def.Image;
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
