using _Game.Scripts.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.LoadSave();
    }
}
