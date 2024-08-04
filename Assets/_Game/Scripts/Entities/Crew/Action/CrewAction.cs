using _Base.Scripts.RPGCommon.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public abstract class CrewActionBase
    {
        public string Name;
        public List<Node> WorkingSlots;
        public bool isAbleToDo;
        public bool IsAbleToDo
        {
            get
            {
                isAbleToDo = Evaluate();
                return isAbleToDo;
            }
        }
        public abstract bool Evaluate();
        public abstract void OnStart();
        public virtual IEnumerator Execute()
        {
            OnStart();
            yield return DoExecute();
            OnExit();
        }
        public abstract IEnumerator DoExecute();

        public abstract void OnExit();
        public abstract void Interupt();
    }
    public class Idle : CrewActionBase
    {
        Crew crew;
        public Node node;

        public Idle(Crew crew, Node node)
        {
            Name = "IDLE";
            this.node = node;
            this.crew = crew;
        }
        public override IEnumerator DoExecute()
        {
            yield return new WaitForSeconds(2);
        }
        public override void Interupt()
        {
            node.State = NodeState.Free;
        }

        public override void OnExit()
        {
            if (node != null)
            {
                node.State = NodeState.Free;

            }
            crew.State = CrewState.Idle;

            crew.body.velocity = Vector2.zero;
            Debug.Log("exit idle");
        }

        public override void OnStart()
        {
            if (node != null)
            {
                node.State = NodeState.Occupied;
            }
            crew.body.velocity = Vector2.zero;
            crew.State = CrewState.Idle;
        }

        public override bool Evaluate()
        {
            return true;
        }
    }
    public class MoveToWorklocation : CrewActionBase
    {
        IWorkLocation workLocation;
        Crew crew;
        Node targetNode;
        public MoveToWorklocation(Crew crew, IWorkLocation workLocation)
        {
            this.crew = crew;
            this.workLocation = workLocation;
        }



        public override IEnumerator DoExecute()
        {
            yield return crew.CrewMovement.MoveTo(targetNode.transform.position);
        }

        public override void Interupt()
        {
            crew.body.velocity = Vector2.zero;
            targetNode.State = NodeState.Free;
        }

        public override void OnExit()
        {
            targetNode.State = NodeState.Free;
            crew.body.velocity = Vector2.zero;
            crew.State = CrewState.Idle;
        }

        public override void OnStart()
        {
            List<Node> availableWorkingSlots = workLocation.WorkingSlots
                .Where(slot => slot.State == NodeState.Free)
                .ToList();

            targetNode = DistanceHelper.GetClosestToPosition(availableWorkingSlots.ToArray(), (slot) => slot, crew.transform.position);
            crew.OccupyingNodes = new System.Collections.Generic.List<Node> { targetNode };
            crew.State = CrewState.Moving;
            targetNode.State = NodeState.Occupied;
        }

        public override bool Evaluate()
        {
            List<Node> availableWorkingSlots = workLocation.WorkingSlots
                .Where(slot => slot.State == NodeState.Free)
                .ToList();
            List<Node> ableToMoveToNode = new List<Node>();
            foreach (var slot in availableWorkingSlots)
            {
                if (crew.CrewMovement.IsAbleToMoveTo(slot))
                    ableToMoveToNode.Add(slot);
            }

            if (ableToMoveToNode.Count > 0)
            {
                targetNode = DistanceHelper.GetClosestToPosition(ableToMoveToNode.ToArray(), (slot) => slot, crew.transform.position);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class MoveToNode : CrewActionBase
    {
        Node targetNode;
        Crew crew;
        public MoveToNode(Crew crew, Node node)
        {
            this.crew = crew;
            this.targetNode = node;
        }

        public override IEnumerator DoExecute()
        {
            yield return crew.CrewMovement.MoveTo(targetNode.transform.position);
        }

        public override bool Evaluate()
        {
            return crew.CrewMovement.IsAbleToMoveTo(targetNode);
        }

        public override void Interupt()
        {
            crew.body.velocity = Vector2.zero;
            targetNode.State = NodeState.Free;
        }

        public override void OnExit()
        {
            targetNode.State = NodeState.Free;
            crew.body.velocity = Vector2.zero;
            crew.State = CrewState.Idle;
        }

        public override void OnStart()
        {
            crew.OccupyingNodes = new System.Collections.Generic.List<Node> { targetNode };
            crew.State = CrewState.Moving;
            targetNode.State = NodeState.Occupied;
        }
    }

    public class RepairCell : CrewActionBase
    {
        Cell cell;
        Crew crew;
        public RepairCell(Crew crew, Cell cell)
        {
            this.crew = crew;
            this.cell = cell;
        }

        public override IEnumerator DoExecute()
        {
            if (crew.transform.position.x < cell.transform.position.x)
            {
                crew.Animation.Flip(Direction.Right);
            }
            else if (crew.transform.position.x > cell.transform.position.x)
            {
                crew.Animation.Flip(Direction.Left);
            }
            IAliveStats aliveStats = cell.Stats as IAliveStats;
            while (!aliveStats.HealthPoint.IsFull)
            {
                aliveStats.HealthPoint.StatValue.BaseValue += crew.stats.RepairSpeed.Value * Time.deltaTime;
                yield return null;
            }
            cell.OnFixed();
        }

        public override bool Evaluate()
        {
            return true;
        }

        public override void Interupt()
        {

        }

        public override void OnExit()
        {
            crew.State = CrewState.Idle;
        }

        public override void OnStart()
        {
            crew.State = CrewState.Reparing;
        }
    }

    public class RepairGridItem : CrewActionBase
    {
        IGridItem cell;
        Crew crew;
        public RepairGridItem(Crew crew, IGridItem cell)
        {
            this.crew = crew;
            this.cell = cell;
        }

        public override IEnumerator DoExecute()
        {
            IAliveStats aliveStats = cell.Stats as IAliveStats;
            while (!aliveStats.HealthPoint.IsFull)
            {
                aliveStats.HealthPoint.StatValue.BaseValue += crew.stats.RepairSpeed.Value * Time.deltaTime;
                yield return null;
            }
        }

        public override bool Evaluate()
        {
            return true;
        }

        public override void Interupt()
        {

        }

        public override void OnExit()
        {
            crew.State = CrewState.Idle;
        }

        public override void OnStart()
        {
            if (crew.transform.position.x < cell.Transform.position.x)
            {
                crew.Animation.Flip(Direction.Right);
            }
            else if (crew.transform.position.x > cell.Transform.position.x)
            {
                crew.Animation.Flip(Direction.Left);
            }
            crew.State = CrewState.Reparing;
        }
    }

    public class RepairCellSplitItemComponent : CrewActionBase
    {
        ICellSplitItemComponent cell;
        Crew crew;
        public RepairCellSplitItemComponent(Crew crew, ICellSplitItemComponent cell)
        {
            this.crew = crew;
            this.cell = cell;
        }

        public override IEnumerator DoExecute()
        {
            IAliveStats aliveStats = cell.Stats as IAliveStats;
            while (!aliveStats.HealthPoint.IsFull)
            {
                aliveStats.HealthPoint.StatValue.BaseValue += crew.stats.RepairSpeed.Value * Time.deltaTime;
                yield return null;
            }
        }

        public override bool Evaluate()
        {
            return true;
        }

        public override void Interupt()
        {

        }

        public override void OnExit()
        {
            crew.State = CrewState.Idle;
        }

        public override void OnStart()
        {
            if (crew.transform.position.x < cell.Transform.position.x)
            {
                crew.Animation.Flip(Direction.Right);
            }
            else if (crew.transform.position.x > cell.Transform.position.x)
            {
                crew.Animation.Flip(Direction.Left);
            }
            crew.State = CrewState.Reparing;
        }
    }
}
