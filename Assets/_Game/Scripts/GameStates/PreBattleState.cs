using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.GameContext;
using _Game.Scripts.Managers;
using _Game.Scripts.UI;
using System.Collections;

namespace _Game.Scripts.GameStates
{
    public class PreBattleState : AbstractState
    {
        public override string Name => nameof(PreBattleState);
        public override void Enter()
        {
            base.Enter();
            if (GameManager.Instance.MapManager.CurrentMap.path.Count > 0)
            {
                ViewManager.Instance.Show<MapView>();
            }
            else
            {
                ViewManager.Instance.Show<PreBattleView>();
            }

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
            playerContext.ManaPoint.MaxValue = 100;
            /* playerContext.ManaPoint = 40;
             playerContext.HealthPoint = 1000;*/
            GlobalContext.PlayerContext = playerContext;
            //
            GameManager.Instance.MapManager.OnGameStart();
        }
    }
}