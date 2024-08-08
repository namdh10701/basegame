using _Base.Scripts.EventSystem;
using _Game.Scripts;
using _Game.Scripts.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CrewJobData : MonoBehaviour
    {
        public static Dictionary<Type, int> DefaultPiority = new Dictionary<Type, int>()
        {
            {typeof(FixCellTask) ,20 },
            {typeof(FixCannonTask), 80},
            {typeof(FixAmmoTask), 70},
            {typeof(FixCarpetTask),60 }
        };
        public List<CrewTask> ActivateJobs = new List<CrewTask>();

        public Action<CrewTask> OnActivateJobsChanged;
        public ShipSetup ShipSetup;

        public Dictionary<Cell, FixCellTask> FixCellJobDic = new Dictionary<Cell, FixCellTask>();
        public Dictionary<Cannon, FixCannonTask> FixCannonJobDic = new Dictionary<Cannon, FixCannonTask>();
        public Dictionary<Ammo, FixAmmoTask> FixAmmoJobDic = new Dictionary<Ammo, FixAmmoTask>();

        public Dictionary<Carpet, FixCarpetTask> FixCarpetJobDic = new Dictionary<Carpet, FixCarpetTask>();

        public List<CrewTask> AllJobs = new List<CrewTask>();
        public List<CrewTask> GetHighestPiorityActiveJobs()
        {
            List<CrewTask> highestPriorityJobs = new List<CrewTask>();
            int highestPriority = -1;

            foreach (CrewTask crewJob in ActivateJobs)
            {
                if (crewJob.Status != TaskStatus.Working && crewJob.Priority > highestPriority)
                {
                    highestPriority = crewJob.Priority;
                }
            }

            foreach (CrewTask crewJob in ActivateJobs)
            {
                if (crewJob.Status != TaskStatus.Working && crewJob.Priority == highestPriority)
                {
                    highestPriorityJobs.Add(crewJob);
                }
            }

            return highestPriorityJobs;
        }

        public void Initialize()
        {
            foreach (Ammo ammo in ShipSetup.Ammos)
            {
                FixAmmoTask fixAmmoTask = new FixAmmoTask(this, ammo);
                FixAmmoJobDic.Add(ammo, fixAmmoTask);
                AllJobs.Add(fixAmmoTask);
            }

            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                FixCannonTask fixCannonTask = new FixCannonTask(this, cannon);
                FixCannonJobDic.Add(cannon, fixCannonTask);
                AllJobs.Add(fixCannonTask);
            }

            foreach (Carpet carpet in ShipSetup.Carpets)
            {
                FixCarpetTask fixCannonTask = new FixCarpetTask(this, carpet);
                FixCarpetJobDic.Add(carpet, fixCannonTask);
                AllJobs.Add(fixCannonTask);
            }

            foreach (Cell cell in ShipSetup.AllCells)
            {
                if (cell.GridItem == null || cell.GridItem is not IEffectTaker)
                {
                    FixCellTask fixCellTask = new FixCellTask(this, cell);
                    FixCellJobDic.Add(cell, fixCellTask);
                    AllJobs.Add(fixCellTask);
                }
            }
        }


        private void Awake()
        {
            GlobalEvent<Carpet, int>.Register("FixCarpet", ActivateFixCarpetTask);
            GlobalEvent<Cell, int>.Register("FixCell", ActivateFixCellTask);
            GlobalEvent<Ammo, int>.Register("FixAmmo", ActivateFixAmmoTask);
            GlobalEvent<Cannon, int>.Register("FixCannon", ActivateFixCannonTask);
        }

        private void OnDestroy()
        {
            GlobalEvent<Carpet, int>.Unregister("FixCarpet", ActivateFixCarpetTask);
            GlobalEvent<Cell, int>.Unregister("FixCell", ActivateFixCellTask);
            GlobalEvent<Ammo, int>.Unregister("FixAmmo", ActivateFixAmmoTask);
            GlobalEvent<Cannon, int>.Unregister("FixCannon", ActivateFixCannonTask);
        }
        void ActivateFixCarpetTask(Carpet cell, int piority)
        {
            FixCarpetTask fixCellJob = FixCarpetJobDic[cell];
            fixCellJob.Priority = piority;
            if (!ActivateJobs.Contains(fixCellJob))
            {
                fixCellJob.Status = TaskStatus.Pending;
                ActivateJobs.Add(fixCellJob);
            }
            Debug.Log("fix carpet job");
            OnActivateJobsChanged?.Invoke(fixCellJob);
        }

        void ActivateFixCellTask(Cell cell, int piority)
        {
            FixCellTask fixCellJob = FixCellJobDic[cell];
            fixCellJob.Priority = piority;
            if (!ActivateJobs.Contains(fixCellJob))
            {
                fixCellJob.Status = TaskStatus.Pending;
                ActivateJobs.Add(fixCellJob);
            }
            OnActivateJobsChanged?.Invoke(fixCellJob);
        }

        public void OnTaskCompleted(CrewTask task)
        {
            if (ActivateJobs.Contains(task))
            {
                ActivateJobs.Remove(task);
                task.Status = TaskStatus.Disabled;
            }
        }


        void ActivateFixAmmoTask(Ammo ammo, int priority)
        {
            FixAmmoTask fixAmmoJob = FixAmmoJobDic[ammo];
            fixAmmoJob.Priority = priority;
            if (!ActivateJobs.Contains(fixAmmoJob))
            {
                fixAmmoJob.Status = TaskStatus.Pending;
                ActivateJobs.Add(fixAmmoJob);

            }
            OnActivateJobsChanged?.Invoke(fixAmmoJob);
        }

        void ActivateFixCannonTask(Cannon ammo, int priority)
        {
            FixCannonTask fixCannonJob = FixCannonJobDic[ammo];
            fixCannonJob.Priority = priority;
            if (!ActivateJobs.Contains(fixCannonJob))
            {
                fixCannonJob.Status = TaskStatus.Pending;
                ActivateJobs.Add(fixCannonJob);
            }
            OnActivateJobsChanged?.Invoke(fixCannonJob);
        }

        public void Clear()
        {
            GlobalEvent<Carpet, int>.Unregister("FixCarpet", ActivateFixCarpetTask);
            GlobalEvent<Cell, int>.Unregister("FixCell", ActivateFixCellTask);
            GlobalEvent<Ammo, int>.Unregister("FixAmmo", ActivateFixAmmoTask);
            GlobalEvent<Cannon, int>.Unregister("FixCannon", ActivateFixCannonTask);
        }

        public void OnRevive()
        {
            GlobalEvent<Carpet, int>.Register("FixCarpet", ActivateFixCarpetTask);
            GlobalEvent<Cell, int>.Register("FixCell", ActivateFixCellTask);
            GlobalEvent<Ammo, int>.Register("FixAmmo", ActivateFixAmmoTask);
            GlobalEvent<Cannon, int>.Register("FixCannon", ActivateFixCannonTask);
        }
    }
}