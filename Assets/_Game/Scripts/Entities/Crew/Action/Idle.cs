using _Game.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : CrewActionBase
{
    Crew crew;
    public Idle(Crew crew)
    {
        Name = "IDLE";
        this.crew = crew;
        this.Execute =  DoExecute();
        this.Interupt = DoInterupt();
    }
    public IEnumerator DoExecute()
    {
        crew.Animation.AddIdle();
        yield return new WaitForSeconds(2);
    }

    public IEnumerator DoInterupt()
    {
        yield break;
    }

    public override void ReBuild(Crew crew)
    {
        this.Execute = DoExecute();
        this.Interupt = DoInterupt();
    }
}
