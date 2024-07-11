using _Game.Scripts.Entities;
using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Game.Scripts.PathFinding;
using _Base.Scripts.EventSystem;

namespace _Game.Features.Gameplay
{
    public class ReloadCannonJob : CrewJob
    {
        public Cannon cannon;
        public Ammo bullet;
        Node workingSlot;
        Node cannonWorkingSlot;
        public ReloadCannonJob(Cannon cannon) : base()
        {
            Name = "RELOAD CANNON ";
            DefaultPiority = 20;
            Piority = 20;

            this.cannon = cannon;
            this.bullet = null;
        }

        public void AssignBullet(Ammo bullet)
        {
            this.bullet = bullet;
            WorkLocation = bullet.GetComponent<IWorkLocation>();
        }
        public override IEnumerator Execute(Crew crew)
        {
            if (crew.CrewAction.CarryingBullet == null || crew.CrewAction.CarryingBullet != bullet)
            {
                List<Node> availableWorkingSlots = WorkLocation.WorkingSlots
               .Where(slot => slot.State == NodeState.Free)
               .ToList();
                workingSlot = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
                workingSlot.State = NodeState.Occupied;
                yield return crew.CrewMovement.MoveTo(workingSlot.transform.position);
                yield return new WaitForSeconds(0.5f);
                workingSlot.State = NodeState.Free;
                if (((ShipStats)crew.Ship.Stats).ManaPoint.Value < bullet.stats.EnergyCost.Value)
                {
                    yield break;
                }
                ((ShipStats)crew.Ship.Stats).ManaPoint.StatValue.BaseValue -= bullet.stats.EnergyCost.Value;
                crew.Carry(bullet);

            }
            List<Node> availableCannonWorkingSlots = cannon.GetComponent<IWorkLocation>().WorkingSlots
              .Where(slot => slot.State == NodeState.Free)
             .ToList();
            cannonWorkingSlot = DistanceHelper.GetClosestToPosition(availableCannonWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
            yield return crew.CrewMovement.MoveCarry(cannonWorkingSlot.transform.position);
            crew.Dropdown();
            yield return new WaitForSeconds(.5f);
            cannon.Reload(bullet);
            crew.CrewAction.CarryingBullet = null;
            GlobalEvent.Send("ReloadCompleted");
        }

        public override void Interupt(Crew crew)
        {
            crew.body.velocity = Vector3.zero;
            if (workingSlot != null)
            {
                workingSlot.State = NodeState.Free;

                if (cannonWorkingSlot != null)
                {
                    cannonWorkingSlot.State = NodeState.Free;
                }
                crew.StopCarry();
            }

        }
    }

}