
using UnityEngine;
using UnityEngine.UI;

public class StatusButton : MonoBehaviour
{
    public Image Selected;
    int _count = 0;
    void OnEnable()
    {
        Selected.gameObject.SetActive(false);
    }
    public void EnableImage()
    {
        Selected.gameObject.SetActive(_count == 0 || _count % 2 == 0);
        _count++;
    }

}
