
using _Base.Scripts.UI;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixCellJob : CrewJob
{
    public _Game.Scripts.Cell cell;
    Node workingSlot;
    public FixCellJob(_Game.Scripts.Cell cell) : base()
    {
        Name = "FIX CELL " + cell.ToString();
        DefaultPiority = 3;
        Piority = 3;
        WorkLocation = cell.GetComponent<IWorkLocation>();
        this.cell = cell;
    }
    public override IEnumerator Execute(Crew crew)
    {
        List<Node> availableWorkingSlots = WorkLocation.WorkingSlots
             .Where(slot => slot.State == NodeState.Free)
             .ToList();
        workingSlot = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
        workingSlot.State = NodeState.Occupied;
        yield return crew.CrewMovement.MoveTo(workingSlot.transform.position);
        if (crew.transform.position.x < cell.transform.position.x)
        {
            crew.Animation.Flip(Direction.Right);
        }
        else if (crew.transform.position.x > cell.transform.position.x)
        {
            crew.Animation.Flip(Direction.Left);
        }
        crew.Animation.PlayFix();
        yield return new WaitForSeconds(3);
        workingSlot.State = NodeState.Free;
        cell.stats.HealthPoint.StatValue.BaseValue = cell.stats.HealthPoint.MaxValue;
        crew.Animation.PlayIdle();
        yield break;
    }

    public override void Interupt(Crew crew)
    {
        crew.body.velocity = Vector3.zero;
        if (workingSlot != null)
        {
            workingSlot.State = NodeState.Free;
        }
    }
}

public class FixGridItemJob : CrewJob
{
    IGridItem gridItem;
    Node workingSlot;
    public FixGridItemJob(IGridItem item, IWorkLocation worklocation)
    {
        DefaultPiority = 50;
        Piority = 50;
        gridItem = item;
        WorkLocation = worklocation;
    }

    public override IEnumerator Execute(Crew crew)
    {
        List<Node> availableWorkingSlots = WorkLocation.WorkingSlots
             .Where(slot => slot.State == NodeState.Free)
             .ToList();
        workingSlot = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
        workingSlot.State = NodeState.Occupied;
        yield return crew.CrewMovement.MoveTo(workingSlot.transform.position);
        if (crew.transform.position.x < gridItem.Transform.position.x)
        {
            crew.Animation.Flip(Direction.Right);
        }
        else if (crew.transform.position.x > gridItem.Transform.position.x)
        {
            crew.Animation.Flip(Direction.Left);
        }
        crew.Animation.PlayFix();
        yield return new WaitForSeconds(3);
        workingSlot.State = NodeState.Free;
        gridItem.OnFixed();
        crew.Animation.PlayIdle();
        yield break;
    }

    public override void Interupt(Crew crew)
    {
        crew.body.velocity = Vector3.zero;
        if (workingSlot != null)
        {
            workingSlot.State = NodeState.Free;
        }
    }
}
