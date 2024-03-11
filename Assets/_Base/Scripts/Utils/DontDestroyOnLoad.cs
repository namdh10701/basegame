using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Util
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
