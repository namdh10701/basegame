using _Base.Scripts.StateMachine;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using System.Collections;

namespace _Game.Scripts.GameStates
{
    public class EndBattleState : AbstractState
    {
        EndBattleView view;
        public override void Enter()
        {
            base.Enter();
            view = ViewManager.Instance.GetView<EndBattleView>();
            ViewManager.Instance.Show(view);
        }

        public override IEnumerator Execute()
        {
            yield break;
        }

    }
}