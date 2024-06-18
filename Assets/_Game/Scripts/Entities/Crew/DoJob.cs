using _Game.Scripts;
using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[MBTNode("Crew/DoJob")]
public class DoJob : Leaf
{
    public BoolReference IsDoingJob;
    public CrewAniamtionHandler CrewAniamtionHandler;
    public ShipReference ShipReference;
    bool isFinished;
    CrewJob crewJob;
    Coroutine coroutine;
    public override void OnEnter()
    {
        base.OnEnter();

        CrewAniamtionHandler.PlayFix();
       // crewJob = ShipReference.Value.CrewJobController.GetNextJob();
        if (crewJob == null)
        {
            isFinished = true;
        }
        else
        {
            StartCoroutine(DoJobCoroutine());
        }
    }
    public override NodeResult Execute()
    {
        return isFinished ? NodeResult.success : NodeResult.running;
    }

    IEnumerator DoJobCoroutine()
    {
        IsDoingJob.Value = true;
        yield return crewJob.Execute();

        IsDoingJob.Value = false;
        isFinished = true;
    }
}
