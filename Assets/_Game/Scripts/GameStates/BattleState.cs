using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.Managers;
using _Game.Scripts.UI;
using System.Collections;
using UnityEngine;
namespace _Game.Scripts.GameStates
{
    public class BattleState : AbstractState
    {
        BattleView battleView;
        public override string Name => nameof(BattleState);
        public override void Enter()
        {
            base.Enter();
            battleView = ViewManager.Instance.GetView<BattleView>();
            ViewManager.Instance.Show(battleView);
        }

        public override IEnumerator Execute()
        {
            yield break;
        }

        public override void Exit()
        {
            base.Exit();

            GameManager.Instance.MapManager.OnGamePassed();
        }
    }
}