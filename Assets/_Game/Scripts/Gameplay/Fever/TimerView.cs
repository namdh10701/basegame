using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    Timer timer;
    public TextMeshProUGUI text;
    public void Init(Timer timer)
    {
        this.timer = timer;
        timer.ElapsedTimeChanged += UpdateText;
    }
    // Update is called once per frame
    void UpdateText(float elapsedTime)
    {
        text.text = timer.TimeString;
    }
}
