using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using System.Collections;
namespace _Game.Scripts.GameStates
{
    public class BattleState : AbstractState
    {
        public override string Name => nameof(BattleState);
        public override void Enter()
        {
            base.Enter();
            ViewManager.Instance.Show<BattleView>();
        }

        public override IEnumerator Execute()
        {
            yield break;
        }
    }
}