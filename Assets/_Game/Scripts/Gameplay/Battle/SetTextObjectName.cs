using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
