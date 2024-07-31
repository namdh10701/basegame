using _Game.Scripts;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System.Collections;
using UnityEngine;
namespace _Game.Features.Gameplay
{
    public abstract class CrewActionBase
    {
        public string Name;
        public abstract bool IsAbleToDo { get; }
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

        public override bool IsAbleToDo => true;

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
    }

    public class MoveToNode : CrewActionBase
    {
        Node targetNode;
        Crew crew;
        public override bool IsAbleToDo => crew.CrewMovement.IsAbleToMoveTo(targetNode);
        public MoveToNode(Crew crew, Node node)
        {
            this.crew = crew;
            this.targetNode = node;
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
            crew.OccupyingNodes = new System.Collections.Generic.List<Node> { targetNode };
            crew.State = CrewState.Moving;
            targetNode.State = NodeState.Occupied;
        }
    }

    public class RepairCell : CrewActionBase
    {
        public override bool IsAbleToDo => true;
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
            yield return new WaitForSeconds(2);
            cell.OnFixed();
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
        public override bool IsAbleToDo => true;
        IGridItem cell;
        Crew crew;
        public RepairGridItem(Crew crew, IGridItem cell)
        {
            this.crew = crew;
            this.cell = cell;
        }

        public override IEnumerator DoExecute()
        {
            yield return new WaitForSeconds(2);
            cell.GridItemStateManager.GridItemState = GridItemState.Active;
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
