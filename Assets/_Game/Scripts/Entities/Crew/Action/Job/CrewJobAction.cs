using System.Collections;
public class CrewJobAction : CrewAction
{
    public CrewJob CrewJob;
    public CrewJobAction(CrewJob crewJob, IEnumerator execute, IEnumerator interupt)
    {
        this.CrewJob = crewJob;
        this.Execute = execute;
        this.Interupt = interupt;
    }
}
