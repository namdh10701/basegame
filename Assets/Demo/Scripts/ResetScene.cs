
using _Base.Scripts.Shared;
using _Base.Scripts.UI.Managers;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetScene : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        ViewManager.Instance.ResetScene();
    }


}
