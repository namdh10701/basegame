using Core.Env;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Delays the state-machine for the set amount
/// </summary>
public class LoadingState : AbstractState
{
    public override string Name => nameof(LoadingState);

    LoadingView loadingView;
    SceneController sceneController;

    public override void Enter()
    {
        base.Enter();
        loadingView = ViewManager.Instance.GetView<LoadingView>();
        ViewManager.Instance.Show(loadingView);
    }

    public override IEnumerator Execute()
    {
        if (Environment.ENV == Environment.Env.DEV)
        {
            yield break;
        }
        /*var op = SceneManager.UnloadSceneAsync(1);
        op.allowSceneActivation = false;*/
        Scene mainMenuScene = SceneManager.GetSceneByBuildIndex(2);
        AsyncOperation asyncOperation;
        asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;
        /*        PurchaseState removeAdsPurchaseState = IAPManager.Instance.RemoveAdsPurchaseState;
        */

        float loadingProgress = 0;
        float timeout = 2f;
        float startTime = Time.time;
        bool openAdShowed = false;
        float minAdditionalTimeout = 1.5f;
        float elapsedTime2 = 0;
        float newSpeed = 1 / minAdditionalTimeout;
        bool isHasInternet = Application.internetReachability != NetworkReachability.NotReachable;
        loadingView.SetProgress(0);
        while (!asyncOperation.allowSceneActivation)
        {

            float elapsedTime = Time.time - startTime;
            if ((asyncOperation.progress >= 0.9f && elapsedTime >= timeout && !openAdShowed)
                || (asyncOperation.progress >= 0.9f && openAdShowed && elapsedTime2 >= minAdditionalTimeout)
                || ((asyncOperation.progress >= 0.9f && (!isHasInternet) && elapsedTime > minAdditionalTimeout)))
            {

                asyncOperation.allowSceneActivation = true;
                yield return null;
                break;
            }

            if (!openAdShowed)
            {
                /*if (!AdsHandler.IsRemovedAdsLocalState)
                {
                    if (AdsController.Instance.IsOpenAdReady)
                    {
                        AdsController.Instance.ShowAppOpenAd();
                        openAdShowed = true;

                    }
                }
                else if (AdsHandler.IsRemovedAdsLocalState)
                {
                    openAdShowed = true;
                }*/
            }

            if (openAdShowed)
            {
                newSpeed = (1 - loadingProgress) / minAdditionalTimeout;
                elapsedTime2 += Time.deltaTime;
                loadingProgress += newSpeed * Time.deltaTime;
                loadingView.SetProgress(loadingProgress);
            }
            else
            {
                if (!isHasInternet)
                {
                    loadingProgress += newSpeed * Time.deltaTime;
                    loadingView.SetProgress(loadingProgress);
                }
                else
                {
                    if (elapsedTime < timeout)
                    {
                        loadingProgress = Mathf.Clamp01(elapsedTime / timeout);
                    }
                    else
                    {
                        loadingProgress = Mathf.Clamp01(asyncOperation.progress / 0.9f + (elapsedTime - timeout) / timeout);
                    }
                    loadingView.SetProgress(loadingProgress);
                }
            }
            yield return null;
        }
        yield break;
    }
    public override void Exit()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        base.Exit();

    }
}
