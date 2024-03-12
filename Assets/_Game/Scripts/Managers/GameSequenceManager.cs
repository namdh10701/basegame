using _Base.Scripts;
using _Base.Scripts.Bootstrap;
using _Base.Scripts.EventSystem;
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
        IState loadingState;
        IState mainMenuState;
        IState levelSelectState;
    
        SceneController sceneController;


        public override void Initialize()
        {
            // sceneController = new SceneController(SceneManager.GetActiveScene());

            initState = new InitState();
            loadingState = new LoadingState();
            mainMenuState = new State(OnMainMenuStateEntered);
            levelSelectState = new State(OnLevelSelectStateEntered);

            initState.AddLink(new Link(loadingState));
            loadingState.AddLink(new Link(levelSelectState));
            mainMenuState.AddLink(new EventLink(LinkEvents.Click_LevelSelect, levelSelectState));
            levelSelectState.AddLink(new EventLink(LinkEvents.Click_MainMenu, mainMenuState));

            gameStateMachine.Run(initState);

        }
        void OnMainMenuStateEntered()
        {
            ViewManager.Instance.Show<MainMenuView>(Transition.CrossFade);
        }

        void OnLevelSelectStateEntered()
        {
            ViewManager.Instance.Show<LevelSelectionView>(Transition.None);
        }

        public class Listener : IGameEventListener<int, int, int>
        {
            public void OnEventRaised(int eventData1, int eventData2, int eventData3)
            {
                Debug.Log(eventData1);
            }
        }
    }
}
