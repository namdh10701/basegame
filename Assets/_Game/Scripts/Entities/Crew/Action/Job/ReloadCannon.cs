using _Game.Scripts.Entities;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Game.Scripts.PathFinding;

public class ReloadCannonJob : CrewJob
{
    public Cannon cannon;
    public Bullet bullet;
    public ReloadCannonJob(Cannon cannon) : base()
    {
        DefaultPiority = 3;
        Piority = 3;
        WorkLocation = bullet.GetComponent<IWorkLocation>();
        this.cannon = cannon;
        this.bullet = null;
    }
    public override IEnumerator Execute(Crew crew)
    {
        crew.Animation.PlayMove();
        List<Node> availableWorkingSlots = WorkLocation.WorkingSlots
            .Where(slot => slot.State == WorkingSlotState.Free)
            .ToList();
        Node workingSlot = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
        workingSlot.State = WorkingSlotState.Occupied;

        yield return crew.CrewMovement.MoveTo(workingSlot.transform.position);
        yield return new WaitForSeconds(0.5f);
        workingSlot.State = WorkingSlotState.Free;
        crew.Carry(bullet);

        List<Node> availableCannonWorkingSlots = cannon.GetComponent<IWorkLocation>().WorkingSlots
          .Where(slot => slot.State == WorkingSlotState.Free)
          .ToList();
        Node cannonWorkingSlot = DistanceHelper.GetClosestToPosition(availableCannonWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
        yield return crew.CrewMovement.MoveTo(cannonWorkingSlot.transform.position);
        yield return new WaitForSeconds(0.5f);
        cannon.Reloader.Reload(bullet);
        crew.StopCarry();
    }

    public override IEnumerator Interupt(Crew crew)
    {
        crew.StopCarry();
        yield break;
    }
}

