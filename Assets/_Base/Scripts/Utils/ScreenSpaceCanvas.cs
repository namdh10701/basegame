using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSpaceCanvas : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }


    private void ChangedActiveScene(Scene arg0, Scene arg1)
    {
        canvas.worldCamera = Camera.main;
    }
}
