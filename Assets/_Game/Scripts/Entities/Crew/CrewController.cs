
using _Game.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class CrewController : MonoBehaviour
    {
        public Ship Ship;
        public CrewJobData CrewJobData;
        public bool HasPendingJob => CrewJobData.ActivateJobs.Count > 0;
        public List<Crew> crews = new List<Crew>();
        private void Awake()
        {
            CrewJobData.OnActivateJobsChanged += OnJobActivate;
        }

        public void AddCrew(Crew crew)
        {
            crews.Add(crew);
            crew.Ship = Ship;
            crew.CrewMovement.pathfinder = Ship.PathfindingController;
        }


        void OnJobActivate(CrewTask task)
        {
            if (task.Priority == int.MaxValue)
            {
                Crew crew = GetMostSuitableCrewForTask(task);
                if (crew != null)
                {
                    AssignTask(crew, task);
                }
            }
            else
            {
                List<Crew> freeCrews = GetFreeCrews();
                if (freeCrews.Count > 0)
                {
                    Crew crew = GetClosetCrewToTask(task, freeCrews);
                    if (crew != null)
                    {
                        AssignTask(crew, task);
                    }
                }
            }
        }

        private void AssignTask(Crew crew, CrewTask task)
        {
            crew.CrewAction.DoTask(task);
        }

        public bool GetMostSuitableTaskForCrew(Crew crew, out CrewTask task)
        {
            task = null;
            List<CrewTask> highestPiorityTasks = CrewJobData.GetHighestPiorityActiveJobs();
            if (highestPiorityTasks.Count == 0) return false;
            task = GetClosetTaskFromCrew(highestPiorityTasks, crew);
            return true;
        }

        CrewTask GetClosetTaskFromCrew(List<CrewTask> tasks, Crew crew)
        {
            float closestDistance = Mathf.Infinity;
            CrewTask closestJob = null;
            foreach (CrewTask task in tasks)
            {
                List<Node> workingSlots = task.StartLocation.WorkingSlots;

                foreach (Node workingSlot in workingSlots)
                {
                    float distanceToJob = Vector2.Distance(workingSlot.transform.position, crew.transform.position);

                    if (distanceToJob < closestDistance)
                    {
                        closestDistance = distanceToJob;
                        closestJob = task;
                    }
                }
            }
            return closestJob;
        }

        Crew GetMostSuitableCrewForTask(CrewTask task)
        {
            if (task.StartLocation.WorkingSlots.Count == 0)
            {
                return null;
            }
            List<Crew> freeCrews = GetFreeCrews();
            if (freeCrews.Count > 0)
            {
                Crew closetCrewToTask = GetClosetCrewToTask(task, freeCrews);
                return closetCrewToTask;
            }
            else
            {
                List<Crew> lowerPriorityCrews = GetCrewsWithLowerTaskPriority(task);
                if (lowerPriorityCrews.Count == 0)
                {
                    return GetClosetCrewToTask(task, crews);
                }
                else
                {
                    Crew closetCrewWithLowerPriority = GetClosetCrewToTask(task, lowerPriorityCrews);
                    return closetCrewWithLowerPriority;
                }

            }
        }
        List<Crew> GetFreeCrews()
        {
            List<Crew> freeCrews = new List<Crew>();

            foreach (Crew crew in crews)
            {
                if (crew.CrewAction.DoingTask == null)
                {
                    freeCrews.Add(crew);
                }
            }
            return freeCrews;
        }
        List<Crew> GetCrewsWithLowerTaskPriority(CrewTask task)
        {
            List<Crew> crews = new List<Crew>();
            foreach (Crew crew in crews)
            {
                if (crew.CrewAction.DoingTask != null)
                {
                    if (crew.CrewAction.DoingTask.Priority < task.Priority)
                    {
                        crews.Add(crew);
                    }
                }

            }
            return crews;
        }

        Crew GetClosetCrewToTask(CrewTask crewJob, List<Crew> crews)
        {
            Crew closestCrew = null;
            float closestDistance = Mathf.Infinity;

            foreach (Crew crew in crews)
            {
                foreach (Node workingSlot in crewJob.StartLocation.WorkingSlots)
                {
                    float distance = Vector2.Distance(crew.transform.position, workingSlot.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestCrew = crew;
                    }
                }
            }
            return closestCrew;
        }

        public void ActivateCrews()
        {
            foreach (Crew crew in crews)
            {
                crew.CrewAction.Activate();
            }
        }
    }
}