using _Base.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCoroutine
{

    private MonoBehaviour monoBehaviour;
    private string coroutineName;
    private Action onCompleted;
    private object value;
    public MyCoroutine(MonoBehaviour monoBehaviour, string coroutineName, object value = null, Action onCompleted = null)
    {
        this.monoBehaviour = monoBehaviour;
        this.coroutineName = coroutineName;
        this.onCompleted = onCompleted;
        this.value = value;
    }

    public void Start()
    {
        Coroutines.StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        Debug.Log("STArt here");
        yield return monoBehaviour.StartCoroutine(coroutineName, value);
        onCompleted.Invoke();
    }
}
