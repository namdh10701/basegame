using _Game.Scripts.Battle;

public interface IRayListener
{
    public void OnIntersectStop(Area bound, DirectionRay ray);
    public void OnIntersectBounds(Area bound, DirectionRay ray, float distance);
    public void OnInsideBounds(Area bound, DirectionRay ray);

    public void OnOutsideBounds(Area bound, DirectionRay ray);
}