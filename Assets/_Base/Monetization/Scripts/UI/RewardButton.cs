using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardButton : Button
{
    public UnityEvent _onRewarded = new UnityEvent();
    public UnityEvent _onRewardFailed = new UnityEvent();
    protected override void Awake()
    {
        base.Awake();
        onClick.AddListener(() => OnClicked());
    }
    private void OnClicked()
    {
        /*AdsController.Instance.ShowReward(watched =>
        {
            Debug.Log("Watchhed " + watched);
            if (watched)
            {
                _onRewarded.Invoke();
            }
            else
            {
                _onRewardFailed.Invoke();
            }
        });*/
        StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSecondsRealtime(2);
        bool Watched = Random.Range(0, 2) == 1;
        if (Watched)
        {
            _onRewarded.Invoke();
            Debug.Log("WATCHED");
        }
        else
        {
            _onRewardFailed.Invoke();
            Debug.Log("NOT WATCHED");
        }
    }
}
