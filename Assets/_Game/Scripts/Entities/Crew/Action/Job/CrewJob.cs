using _Game.Scripts;
using System;
using System.Collections;
using static UnityEngine.CullingGroup;

namespace _Game.Features.Gameplay
{
    public enum JobStatus
    {
        Deactive, Free, WorkingOn
    }
    [Serializable]
    public abstract class CrewJob
    {
        public abstract string Name { get; }
        public int Piority;
        private JobStatus jobStatus;
        public JobStatus Status { get => jobStatus; set { jobStatus = value; StatusChanged?.Invoke(jobStatus); } }
        public IWorkLocation WorkLocation;
        public Action<JobStatus> StatusChanged;
        Crew crew;
        public CrewJob()
        {
            Status = JobStatus.Free;
        }
        public CrewJobAction BuildCrewAction(Crew crew)
        {
            this.crew = crew;
            IEnumerator executeCoroutine = DoExecute(crew);
            CrewJobAction crewJobAction = new CrewJobAction(this, executeCoroutine);
            return crewJobAction;
        }

        public IEnumerator DoExecute(Crew crew)
        {
            Status = JobStatus.WorkingOn;
            StatusChanged?.Invoke(Status);
            yield return Execute(crew);
            Status = JobStatus.Deactive;
            StatusChanged?.Invoke(Status);
        }
        public void DoInterupt()
        {
            Interupt(crew);
            Status = JobStatus.Free;
            StatusChanged?.Invoke(Status);
        }

        public abstract IEnumerator Execute(Crew crew);
        public abstract void Interupt(Crew crew);
    }
}