using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Base.Scripts.UI
{
    [ExecuteInEditMode]
    public class SetTextObjectName : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<TextMeshProUGUI>().text = gameObject.name;
        }
        private void OnValidate()
        {
            GetComponent<TextMeshProUGUI>().text = gameObject.name;
        }
    }
}
