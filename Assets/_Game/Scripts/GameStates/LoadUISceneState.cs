using _Base.Scripts.StateMachine;
using _Game.Scripts.Bootstrap;
using System.Collections;
public class LoadUISceneState : AbstractState
{
    string scene;
    public LoadUISceneState(string scene)
    {
        this.scene = scene;
    }
    public override IEnumerator Execute()
    {
        yield return Game.Instance.SceneController.LoadUIScene(scene);
    }
}