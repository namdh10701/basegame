using UnityEngine;

public class DebugTimescale : MonoBehaviour
{
    public float timeScale;
    private void Update()
    {
        Time.timeScale = timeScale;
    }
}
