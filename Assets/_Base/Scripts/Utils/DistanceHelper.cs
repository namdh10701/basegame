using System;
using UnityEngine;

public static class DistanceHelper
{
    public static T GetClosetToPosition<T>(T[] transforms, Vector3 position) where T : MonoBehaviour
    {
        float minDistance = Mathf.Infinity;
        T ret = null;
        foreach (T t in transforms)
        {
            float distance = Vector2.Distance(t.transform.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                ret = t;
            }
        }
        return ret;
    }

    public static T GetClosestToPosition<T>(T[] array, Func<T, MonoBehaviour> getMonoBehaviour, Vector3 position)
    {
        if (array == null || array.Length == 0)
        {
            Debug.LogWarning("Array is null or empty.");
            return default;
        }

        float minDistance = Mathf.Infinity;
        T closestItem = default;

        foreach (T item in array)
        {
            MonoBehaviour mb = getMonoBehaviour(item);
            if (mb == null)
            {
                Debug.LogWarning($"MonoBehaviour field extracted from {typeof(T).Name} instance is null.");
                continue;
            }

            float distance = Vector3.Distance(mb.transform.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestItem = item;
            }
        }

        return closestItem;
    }
}