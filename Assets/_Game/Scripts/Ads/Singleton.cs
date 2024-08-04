using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	public static T Instance
	{
		get
		{
			if (Singleton<T>._instance == null)
			{
				Singleton<T>._instance = UnityEngine.Object.FindObjectOfType<T>();
				if (Singleton<T>._instance == null)
				{
					GameObject gameObject = new GameObject();
					Singleton<T>._instance = gameObject.AddComponent<T>();
				}
			}
			return Singleton<T>._instance;
		}
	}

	protected virtual void Awake()
	{
		if (Singleton<T>._instance == null)
		{
			Singleton<T>._instance = (this as T);
			UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
			this.OnAwake();
		}
		else if (this != Singleton<T>._instance)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	protected virtual void OnAwake()
	{
	}

	protected static T _instance;
}
