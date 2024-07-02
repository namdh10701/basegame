using _Game.Scripts.SaveLoad;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.LoadSave();
    }
}
