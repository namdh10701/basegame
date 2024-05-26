using _Game.Scripts.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctaDirectionRay : MonoBehaviour, IRayListener
{
    public List<DirectionRay> DirectionRay;
    public List<DirectionRay> IntersectingRays;
    public List<DirectionRay> NotIntersectingRays;
    public Action OnChanged;
    private void Awake()
    {
        foreach (DirectionRay directionRay in DirectionRay)
        {
            directionRay.Listener = this;
        }
    }

    private void FixedUpdate()
    {

    }

    public void OnIntersectBounds(Area bound, DirectionRay ray, float distance)
    {
        if (!IntersectingRays.Contains(ray))
        {
            IntersectingRays.Add(ray);
        }
        if (NotIntersectingRays.Contains(ray))
        {
            NotIntersectingRays.Remove(ray);
            OnChanged?.Invoke();
        }
        
    }

    public void OnInsideBounds(Area bound, DirectionRay ray, float distance)
    {
    }

    public void OnIntersectStop(Area bound, DirectionRay ray)
    {
        if (IntersectingRays.Contains(ray))
        {
            IntersectingRays.Remove(ray);
        }
        if (!NotIntersectingRays.Contains(ray))
        {
            NotIntersectingRays.Add(ray);
            
        }
    }

}


