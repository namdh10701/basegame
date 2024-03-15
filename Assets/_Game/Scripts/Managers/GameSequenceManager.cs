using _Base.Scripts;
using _Base.Scripts.Bootstrap;
using _Base.Scripts.EventSystem;
using _Base.Scripts.Shared;
using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.GameStates;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Managers
{
    public class GameSequenceManager : SequenceManager
    {
        readonly StateMachine gameStateMachine = new();

        IState initState;
        IState gameplayState;
        public override void Initialize()
        {
            initState = new InitState();
            gameplayState = new State(OnGameplayStateEntered);
            
            initState.AddLink(new Link(gameplayState));
            CreateGameplayStates();

            gameStateMachine.Run(initState);




        }

        void CreateGameplayStates()
        {
            var setupShipState = new State(OnSetupShipStateEntered);
            var playState = new State(OnPlayStateEntered);
            var endplayState = new State(OnEndPlayStateEntered);

            gameplayState.AddLink(new Link(setupShipState));
            setupShipState.AddLink(new EventLink(LinkEvents.Setup_Ship_Completed, playState));
            playState.AddLink(new EventLink(LinkEvents.Play_End, endplayState));
        }

        void OnGameplayStateEntered()
        {
            ViewManager.Instance.Show<ShipSetupView>();
        }
        void OnSetupShipStateEntered()
        {

        }
        void OnPlayStateEntered()
        {

        }
        void OnEndPlayStateEntered()
        {

        }
    }
}
