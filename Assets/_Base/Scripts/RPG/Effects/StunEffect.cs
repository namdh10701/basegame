using _Base.Scripts.RPG.Effects;
using _Base.Scripts.RPG.Entities;

public class StunEffect : OneShotEffect
{
    public float StunDuration = 1;

    protected override void OnApply(IEffectTaker entity)
    {
        if (entity is IStunable stunable)
        {
            stunable.OnStun(StunDuration);
        }
    }
}
