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
    Node workingSlot;
    Node cannonWorkingSlot;
    public ReloadCannonJob(Cannon cannon) : base()
    {
        Name = "RELOAD CANNON ";
        DefaultPiority = 3;
        Piority = 3;

        this.cannon = cannon;
        this.bullet = null;
    }

    public void AssignBullet(Bullet bullet)
    {
        this.bullet = bullet;
        WorkLocation = bullet.GetComponent<IWorkLocation>();
    }
    public override IEnumerator Execute(Crew crew)
    {
        if (crew.CarryingBullet == null || crew.CarryingBullet != bullet)
        {
            List<Node> availableWorkingSlots = WorkLocation.WorkingSlots
           .Where(slot => slot.State == WorkingSlotState.Free)
           .ToList();
            workingSlot = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
            workingSlot.State = WorkingSlotState.Occupied;
            yield return crew.CrewMovement.MoveTo(workingSlot.transform.position);
            yield return new WaitForSeconds(0.5f);
            workingSlot.State = WorkingSlotState.Free;
            crew.Carry(bullet);
        }
        List<Node> availableCannonWorkingSlots = cannon.GetComponent<IWorkLocation>().WorkingSlots
          .Where(slot => slot.State == WorkingSlotState.Free)
          .ToList();
        cannonWorkingSlot = DistanceHelper.GetClosestToPosition(availableCannonWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
        yield return crew.CrewMovement.MoveCarry(cannonWorkingSlot.transform.position);
        yield return new WaitForSeconds(0.5f);
        cannon.Reloader.Reload(bullet);
        crew.StopCarry();
    }

    public override IEnumerator Interupt(Crew crew)
    {
        crew.body.velocity = Vector3.zero;
        if (workingSlot != null)
        {
            workingSlot.State = WorkingSlotState.Free;
        }
        if (cannonWorkingSlot != null)
        {
            cannonWorkingSlot.State = WorkingSlotState.Free;
        }
        crew.StopCarry();
        yield break;
    }

}

