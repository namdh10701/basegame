using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : View
{
    [SerializeField] Image progressBar;

    public void SetProgress(float amount)
    {
        progressBar.fillAmount = amount;
    }
}
