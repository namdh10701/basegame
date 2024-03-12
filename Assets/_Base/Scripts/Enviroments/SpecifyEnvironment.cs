using UnityEngine;
using static _Base.Scripts.Enviroments.Environment;

namespace _Base.Scripts.Enviroments
{
    public class SpecifyEnvironment : MonoBehaviour
    {
        [SerializeField] Environment.Env env;
        [SerializeField] bool enableDebugLog;

        private void Awake()
        {
            SetEnvironment(env);
            Debug.unityLogger.logEnabled = enableDebugLog;
        }

    }
}
