
using _Base.Scripts.UI;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public class FixCellJob : CrewJob
    {
        public override string Name => nameof(FixCellJob);
        public Cell cell;
        Node workingSlot;
        public FixCellJob(Cell cell) : base()
        {
            Piority = CrewJobData.DefaultPiority[typeof(FixCellJob)];
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

    public abstract class FixGridItemJob : CrewJob
    {
        IGridItem gridItem;
        Node workingSlot;
        public FixGridItemJob(IGridItem item, IWorkLocation worklocation)
        {
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

    public class FixCannonJob : FixGridItemJob
    {
        public override string Name => nameof(FixCannonJob);
        public FixCannonJob(IGridItem item, IWorkLocation worklocation) : base(item, worklocation)
        {
            Piority = CrewJobData.DefaultPiority[typeof(FixCannonJob)];
        }

    }

    public class FixAmmoJob : FixGridItemJob
    {
        public override string Name => nameof(FixAmmoJob);
        public FixAmmoJob(IGridItem item, IWorkLocation worklocation) : base(item, worklocation)
        {
            Piority = CrewJobData.DefaultPiority[typeof(FixAmmoJob)];
        }

        
    }
}