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
            {typeof(ReloadCannonJob) ,100 },
            {typeof(FixCellJob),50 },
            {typeof(FixCannonJob), 80},
            {typeof(FixAmmoJob), 70}
        };

        public List<CrewJob> ActivateJobs = new List<CrewJob>();

        public Action<CrewJob> OnActivateJobsChanged;
        public ShipSetup ShipSetup;

        public Dictionary<Cell, FixCellJob> FixCellJobDic = new Dictionary<Cell, FixCellJob>();
        public Dictionary<Cannon, FixCannonJob> FixCannonJobDic = new Dictionary<Cannon, FixCannonJob>();
        public Dictionary<Ammo, FixAmmoJob> FixAmmoJobDic = new Dictionary<Ammo, FixAmmoJob>();


        public List<CrewJob> AllJobs = new List<CrewJob>();
        public List<CrewJob> GetHighestPiorityActiveJobs()
        {
            List<CrewJob> highestPriorityJobs = new List<CrewJob>();
            int highestPriority = -1;

            foreach (CrewJob crewJob in ActivateJobs)
            {
                if (crewJob.Status != JobStatus.WorkingOn && crewJob.Piority > highestPriority)
                {
                    highestPriority = crewJob.Piority;
                }
            }

            foreach (CrewJob crewJob in ActivateJobs)
            {
                if (crewJob.Status != JobStatus.WorkingOn && crewJob.Piority == highestPriority)
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
                IGridItem item = ammo.GetComponent<IGridItem>();
                IWorkLocation worklocation = ammo.GetComponent<IWorkLocation>();
                FixAmmoJob fixAmmoJob = new FixAmmoJob(item, worklocation);
                FixAmmoJobDic.Add(ammo, fixAmmoJob);
                AllJobs.Add(fixAmmoJob);
            }

            foreach (Cannon cannon in ShipSetup.Cannons)
            {
                IGridItem item = cannon.GetComponent<IGridItem>();

                IWorkLocation worklocation = cannon.GetComponent<IWorkLocation>();
                FixCannonJob fixCannonJob = new FixCannonJob(item, worklocation);
                FixCannonJobDic.Add(cannon, fixCannonJob);
                AllJobs.Add(fixCannonJob);
            }

            foreach (Cell cell in ShipSetup.AllCells)
            {
                if (cell.GridItem == null)
                {
                    FixCellJob fixCellJob = new FixCellJob(cell);
                    FixCellJobDic.Add(cell, fixCellJob);
                    AllJobs.Add(fixCellJob);
                }
            }
        }

        private void Awake()
        {
            GlobalEvent<Cell, int>.Register("FixCell", ActivateFixCellJob);
            GlobalEvent<Ammo, int>.Register("FixAmmo", ActivateFixAmmoJob);
            GlobalEvent<Cannon, int>.Register("FixCannon", ActivateFixCannonJob);
        }

        private void OnDestroy()
        {
            GlobalEvent<Cell, int>.Unregister("FixCell", ActivateFixCellJob);
            GlobalEvent<Ammo, int>.Unregister("FixAmmo", ActivateFixAmmoJob);
            GlobalEvent<Cannon, int>.Unregister("FixCannon", ActivateFixCannonJob);
        }

        void ActivateFixCellJob(Cell cell, int piority)
        {
            FixCellJob fixCellJob = FixCellJobDic[cell];
            fixCellJob.Piority = piority;
            OnActivateJobsChanged.Invoke(fixCellJob);

            if (!ActivateJobs.Contains(fixCellJob))
            {
                ActivateJobs.Add(fixCellJob);
                OnActivateJobsChanged?.Invoke(fixCellJob);
            }
            if (fixCellJob.Status == JobStatus.WorkingOn)
            {
                return;
            }

        }


        void ActivateFixAmmoJob(Ammo ammo, int priority)
        {
            FixAmmoJob fixAmmoJob = FixAmmoJobDic[ammo];
            fixAmmoJob.Piority = priority;
            OnActivateJobsChanged?.Invoke(fixAmmoJob);

            if (!ActivateJobs.Contains(fixAmmoJob))
            {
                fixAmmoJob.Status = JobStatus.Free;
                ActivateJobs.Add(fixAmmoJob);
            }
            if (fixAmmoJob.Status == JobStatus.WorkingOn)
            {
                return;
            }
        }

        void ActivateFixCannonJob(Cannon ammo, int priority)
        {
            FixCannonJob fixCannonJob = FixCannonJobDic[ammo];
            fixCannonJob.Piority = priority;
        }
    }
}