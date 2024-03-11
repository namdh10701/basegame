using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequenceManager : AbstractSingleton<SequenceManager>
{

    [SerializeField] GameObject[] preloadedAssets;
    [SerializeField] Transform persistentRoot;


    readonly StateMachine gameStateMachine = new();

    IState initState;
    IState loadingState;
    IState mainMenuState;
    IState levelSelectState;

    protected override void Awake()
    {
        base.Awake();
    }
    SceneController sceneController;


    public void Initialize()
    {
        sceneController = new SceneController(SceneManager.GetActiveScene());

        initState = new InitState(preloadedAssets, sceneController);
        loadingState = new LoadingState();
        mainMenuState = new State(OnMainMenuStateEntered);
        levelSelectState = new State(OnLevelSelectStateEntered);

        initState.AddLink(new Link(loadingState));
        loadingState.AddLink(new Link(levelSelectState));
        mainMenuState.AddLink(new EventLink(LinkEvent.Click_LevelSelect, levelSelectState));
        levelSelectState.AddLink(new EventLink(LinkEvent.Click_MainMenu, mainMenuState));

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
