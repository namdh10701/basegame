using _Game.Scripts.Battle;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OctaDirectionRay : MonoBehaviour, IRayListener
{

    public List<DirectionRay> DirectionRay;
    public List<DirectionRay> IntersectingRays;
    public List<DirectionRay> NotIntersectingRays;
    public List<DirectionRay> OutsideBoundsRays = new List<DirectionRay>();
    public bool IsInBounds;
    public Action OnChanged;

    public void SetArea(Area area)
    {
        foreach (DirectionRay directionRays in DirectionRay)
        {
            directionRays.area = area;
        }
    }

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

    public void OnInsideBounds(Area bound, DirectionRay ray)
    {
        if (OutsideBoundsRays.Contains(ray))
        {

            IsInBounds = true;
            OutsideBoundsRays.Remove(ray);
        }
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
            OnChanged?.Invoke();
        }
    }

    public void OnOutsideBounds(Area bound, DirectionRay ray)
    {
        if (!OutsideBoundsRays.Contains(ray))
        {
            OutsideBoundsRays.Add(ray);
            if (OutsideBoundsRays.Count == 8)
            {
                IsInBounds = false;
            }
        }
    }
}


