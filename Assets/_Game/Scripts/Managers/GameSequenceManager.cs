using _Base.Scripts.Bootstrap;
using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.GameStates;
using _Game.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    public class GameSequenceManager : SequenceManager
    {
        [Header("Scene Refs")]
        [SerializeField] SceneRef homeScene;
        [SerializeField] SceneRef preBattleScene;
        [SerializeField] SceneRef battleScene;
        [SerializeField] SceneRef battleUIScene;
        [SerializeField] SceneRef endBattleScene;

        [Header("Scene Controller")]
        [SerializeField] GameSceneController gameSceneController;



        readonly StateMachine gameStateMachine = new();

        //Transit State
        IState transit_SpashToHome;
        IState transit_HomeToPrebattle;
        IState transit_PreBattleToHome;
        IState transit_PreBattleToBattle;
        IState transit_BattleToPreBattle;
        //Scene State
        IState initState;
        IState homeState;
        IState preBattleState;
        IState battleState;
        IState endBattleState;
        public override void Initialize()
        {
            initState = new InitState();
            homeState = new HomeState();
            preBattleState = new PreBattleState();
            battleState = new BattleState();
            endBattleState = new EndBattleState();

            transit_SpashToHome = new TransitState(homeScene, null, Transition.CrossFade, gameSceneController);
            transit_HomeToPrebattle = new TransitState(preBattleScene, null, Transition.CrossFade, gameSceneController);
            transit_PreBattleToBattle = new TransitState(battleScene, battleUIScene, Transition.CrossFade, gameSceneController);
            transit_PreBattleToHome = new TransitState(homeScene, null, Transition.CrossFade, gameSceneController);
            transit_BattleToPreBattle = new TransitState(preBattleScene, null, Transition.CrossFade, gameSceneController);

            initState.AddLink(new Link(transit_SpashToHome));
            transit_SpashToHome.AddLink(new Link(homeState));
            homeState.AddLink(new EventLink(LinkEvents.Click_PreBattle, transit_HomeToPrebattle));
            transit_HomeToPrebattle.AddLink(new Link(preBattleState));
            preBattleState.AddLink(new EventLink(LinkEvents.Click_Back, transit_PreBattleToHome));
            preBattleState.AddLink(new EventLink(LinkEvents.Click_Play, transit_PreBattleToBattle));
            transit_PreBattleToHome.AddLink(new Link(homeState));
            transit_PreBattleToBattle.AddLink(new Link(battleState));

            battleState.AddLink(new EventLink(LinkEvents.End_Battle, endBattleState));
            endBattleState.AddLink(new EventLink(LinkEvents.Click_PreBattle, transit_BattleToPreBattle));
            transit_BattleToPreBattle.AddLink(new Link(preBattleState));

            gameStateMachine.Run(initState);

        }

        void OnHomeStateEnter()
        {
            ViewManager.Instance.Show<HomeView>();
            GameObject.Destroy(GameObject.Find("SplashView"));
        }
    }
}
