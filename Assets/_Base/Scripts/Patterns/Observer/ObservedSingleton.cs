﻿using UnityEngine;
public class ObservedSingleton<T> : ObservedSubject where T : Component
{
    static T s_Instance;
    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType<T>();
                if (s_Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    s_Instance = obj.AddComponent<T>();
                }
            }

            return s_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}