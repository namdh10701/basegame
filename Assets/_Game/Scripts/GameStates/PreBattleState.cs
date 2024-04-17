using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.GameContext;
using _Game.Scripts.UI;
using System.Collections;
using UnityEngine;
namespace _Game.Scripts.GameStates
{
    public class PreBattleState : AbstractState
    {
        public override string Name => nameof(PreBattleState);
        public override void Enter()
        {
            base.Enter();
            Debug.Log("ENTER PRE BATTLE");
            ViewManager.Instance.Show<PreBattleView>();
        }
        public override IEnumerator Execute()
        {
            yield break;
        }
        public override void Exit()
        {
            base.Exit();
            //chọn tàu, crew, các thứ xong thì tính toán lại máu, mana thuyền set vào context
            PlayerContext playerContext = new PlayerContext();
            playerContext.MaxManaPoint = 100;
            /* playerContext.ManaPoint = 40;
             playerContext.HealthPoint = 1000;*/
            GlobalContext.PlayerContext = playerContext;
            //
        }
    }
}