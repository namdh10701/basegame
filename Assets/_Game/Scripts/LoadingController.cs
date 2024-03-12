using System.Collections;
using _Base.Scripts.Enviroments;
using _Base.Scripts.UI.Managers;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts
{
    public class LoadingController : MonoBehaviour
    {
        LoadingView loadingView;
        public void Start()
        {
            if (Environment.ENV == Environment.Env.DEV)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                loadingView = ViewManager.Instance.GetView<LoadingView>();
                ViewManager.Instance.Show(loadingView);
                StartCoroutine(LoadGameCoroutine());
            }
        }

        IEnumerator LoadGameCoroutine()
        {
            AsyncOperation asyncOperation;
            var asyncUnload = SceneManager.UnloadSceneAsync(1);
            asyncUnload.allowSceneActivation = true;

            asyncOperation = SceneManager.LoadSceneAsync(2);
            asyncOperation.allowSceneActivation = false;
            /*        PurchaseState removeAdsPurchaseState = IAPManager.Instance.RemoveAdsPurchaseState;
        */

            float loadingProgress = 0;
            float timeout = 5.3f;
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
                    Scene scene = SceneManager.GetSceneByBuildIndex(2);
                    SceneManager.SetActiveScene(scene);
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
    }
}
