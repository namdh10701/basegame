
using _Base.Scripts.UI;
using _Game.Features.Gameplay;
using _Game.Scripts.Entities;
using _Game.Scripts.PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Features.Gameplay
{
    public enum TaskStatus
    {
        Disabled, Pending, Working
    }
    public abstract class CrewTask
    {
        public CrewJobData crewJobData;
        public TaskStatus status;
        public Node workingSlot;
        public TaskStatus Status
        {
            get => status; set
            {
                TaskStatus lastState = status;

                status = value;
                if (status != lastState)
                {
                    OnStatusChanged?.Invoke(value);
                }
            }
        }
        public Crew crew;
        public IWorkLocation StartLocation;
        public Queue<CrewActionBase> CrewActions = new Queue<CrewActionBase>();
        public int Priority;

        public Action<TaskStatus> OnStatusChanged;
        public CrewTask(CrewJobData crewJobData, IWorkLocation startLocation)
        {
            this.crewJobData = crewJobData;
            this.StartLocation = startLocation;
        }
        public void OnCompleted()
        {
            crewJobData.OnTaskCompleted(this);
        }
        public abstract void BuildCrewActions(Crew crew);
        public virtual void RegisterCrew(Crew crew)
        {
            this.crew = crew;
            CrewActions.Clear();
            BuildCrewActions(crew);
        }
    }

    public class FixCellTask : CrewTask
    {
        Cell cell;

        public FixCellTask(CrewJobData crewJobData, Cell cell) : base(crewJobData, cell)
        {
            this.cell = cell;
            Priority = CrewJobData.DefaultPiority[typeof(FixCellTask)];

        }

        public override void BuildCrewActions(Crew crew)
        {
            CrewActions.Enqueue(new MoveToWorklocation(crew, cell));
            CrewActions.Enqueue(new RepairCell(crew, cell));
        }
    }



    public class FixCannonTask : CrewTask
    {
        Cannon cannon;
        public FixCannonTask(CrewJobData crewJobData, Cannon cannon) : base(crewJobData, cannon)
        {
            Priority = CrewJobData.DefaultPiority[typeof(FixCannonTask)];
            this.cannon = cannon;
        }

        public override void BuildCrewActions(Crew crew)
        {
            CrewActions.Enqueue(new MoveToWorklocation(crew, cannon));
            CrewActions.Enqueue(new RepairGridItem(crew, cannon.GetComponent<IGridItem>()));
        }
    }

    public class FixAmmoTask : CrewTask
    {
        public Ammo ammo;
        public FixAmmoTask(CrewJobData crewJobData, Ammo ammo) : base(crewJobData, ammo)
        {
            Priority = CrewJobData.DefaultPiority[typeof(FixCannonTask)];
            this.ammo = ammo;
        }

        public override void BuildCrewActions(Crew crew)
        {
            CrewActions.Enqueue(new MoveToWorklocation(crew, ammo));
            CrewActions.Enqueue(new RepairGridItem(crew, ammo.GetComponent<IGridItem>()));
        }
    }

    public class FixCarpetTask : CrewTask
    {
        public Carpet carpet;
        public FixCarpetTask(CrewJobData crewJobData, Carpet carpet) : base(crewJobData, carpet)
        {
            Priority = CrewJobData.DefaultPiority[typeof(FixCarpetTask)];
            this.carpet = carpet;
        }

        public override void BuildCrewActions(Crew crew)
        {
            foreach (ICellSplitItemComponent cell in carpet.components)
            {
                CrewActions.Enqueue(new MoveToWorklocation(crew, cell as IWorkLocation));
                CrewActions.Enqueue(new RepairCellSplitItemComponent(crew, cell));
            }
        }
    }
}