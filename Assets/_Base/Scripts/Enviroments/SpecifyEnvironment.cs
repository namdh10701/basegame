using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Core.Env.Environment;

public class SpecifyEnvironment : MonoBehaviour
{
    [SerializeField] Env env;
    [SerializeField] bool enableDebugLog;

    private void Awake()
    {
        SetEnvironment(env);
        Debug.unityLogger.logEnabled = enableDebugLog;
    }

}
