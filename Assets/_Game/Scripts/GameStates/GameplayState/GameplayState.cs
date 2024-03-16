using _Base.Scripts.Bootstrap;
using _Base.Scripts.StateMachine;
using _Game.Scripts;
using System.Collections;
using UnityEngine;

/*public class GameplayState : AbstractState
{
   *//* readonly StateMachine gameplayStateMachine = new();
    IState setupShipState;
    IState playState;
    public GameplayState()
    {
        setupShipState = new SetupShipState();
        playState = new PlayState
        setupShipState.AddLink(new EventLink(LinkEvents.Setup_Ship_Completed, playState));
    }
    public override IEnumerator Execute()
    {
        gameplayStateMachine.Run(setupShipState);
        yield return new WaitUntil(() => !gameplayStateMachine.IsRunning);
    }*//*
}*/