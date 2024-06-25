using _Game.Scripts;
using System.Collections;
public class CrewJobAction : CrewAction
{
    public CrewJob CrewJob;

    public CrewJobAction(CrewJob crewJob, IEnumerator execute, IEnumerator interupt)
    {
        Name = crewJob.Name;
        this.CrewJob = crewJob;
        this.Execute = execute;
        this.Interupt = interupt;
    }

    public override void ReBuild(Crew crew)
    {
        this.Execute = CrewJob.DoExecute(crew);
        this.Interupt = CrewJob.DoInterupt(crew);
    }
}
