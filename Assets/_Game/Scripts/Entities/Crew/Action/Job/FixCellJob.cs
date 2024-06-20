using _Game.Scripts;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixCellJob : CrewJob
{
    public Cell cell;
    public FixCellJob(Cell cell) : base()
    {
        DefaultPiority = 3;
        Piority = 3;
        WorkLocation = cell.GetComponent<IWorkLocation>();
        this.cell = cell;
    }
    public override IEnumerator Execute(Crew crew)
    {
        List<Node> availableWorkingSlots = WorkLocation.WorkingSlots
             .Where(slot => slot.State == WorkingSlotState.Free)
             .ToList();
        Node workingSlot = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
        yield return crew.CrewMovement.MoveTo(workingSlot.transform.position);
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
