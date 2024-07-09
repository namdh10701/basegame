using _Game.Scripts;
using System.Collections;
namespace _Game.Features.Gameplay
{
    public class CrewJobAction : CrewActionBase
    {
        public CrewJob CrewJob;

        public CrewJobAction(CrewJob crewJob, IEnumerator execute)
        {
            Name = crewJob.Name;
            this.CrewJob = crewJob;
            this.Execute = execute;
        }

        public override void Interupt()
        {
            CrewJob.DoInterupt();
        }

        public override void ReBuild(Crew crew)
        {
            this.Execute = CrewJob.DoExecute(crew);
        }
    }
}