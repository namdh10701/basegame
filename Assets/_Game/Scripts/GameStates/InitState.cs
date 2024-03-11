using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Delays the state-machine for the set amount
/// </summary>
public class InitState : AbstractState
{
    private GameObject[] preloadedAssets;
    private SceneController sceneController;


    public InitState(GameObject[] preloadedAssets, SceneController sceneController)
    {
        this.sceneController = sceneController;
        this.preloadedAssets = preloadedAssets;
    }

    public override string Name => nameof(DelayState);

    public override IEnumerator Execute()
    {
        foreach (GameObject preloadedAsset in preloadedAssets)
        {
            Object.Instantiate(preloadedAsset, SequenceManager.Instance.transform);
        }
        GameManager.Instance.LoadDatabase();
        GameManager.Instance.LoadSave();
        yield return sceneController.LoadUIScene();
    }


}
