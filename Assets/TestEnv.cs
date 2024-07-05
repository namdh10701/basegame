using _Game.Scripts.DB;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEnv : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Database.Load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
