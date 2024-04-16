using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using System.Collections;
using UnityEngine;
namespace _Game.Scripts.GameStates
{
    public class HomeState : AbstractState
    {
        bool isFirstEnter = true; 
        public override string Name => nameof(HomeState);
        public override void Enter()
        {
            base.Enter();
            if (isFirstEnter)
            {
                isFirstEnter = false;
                GameObject.Destroy(GameObject.Find("SplashView").gameObject);
            }
            ViewManager.Instance.Show<HomeView>();
        }
        public override IEnumerator Execute()
        {
            yield break;
        }
    }
}