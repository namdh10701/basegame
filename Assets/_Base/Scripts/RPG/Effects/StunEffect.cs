using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;

public class StunEffect : OneShotEffect
{
    public float StunDuration = 1;

    protected override void OnApply(Entity entity)
    {
        if (entity.TryGetComponent(out IStunable stunable))
        {
            stunable.OnStun(StunDuration);
        }
    }
}
