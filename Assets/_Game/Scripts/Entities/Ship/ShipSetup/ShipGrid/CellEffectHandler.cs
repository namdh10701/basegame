using _Base.Scripts.RPG.Effects;

public class CellEffectHandler : EffectHandler
{
    public override void Apply(Effect effect)
    {
        effect = Instantiate(effect, null);
        effect.enabled = true;
        effect.transform.position = transform.position;
        if (effect is IProbabilityEffect probEffect)
        {
            float rand = UnityEngine.Random.Range(0.0f, 1.0f);
            if (rand > probEffect.Prob)
            {
                return;
            }
        }
        if (effect is UnstackableEffect newUnStackableEffect)
        {
            foreach (Effect ef in effects.ToArray())
            {
                if (ef is UnstackableEffect exist && exist.Id == newUnStackableEffect.Id)
                {
                    exist.RefreshEffect(newUnStackableEffect);
                    newUnStackableEffect.transform.parent = null;
                    Destroy(newUnStackableEffect.gameObject);
                    return;
                }
            }
        }
        effect.OnEnded += OnEffectEnded;
        effect.Apply(EffectTaker);

        if (effect is TimeoutEffect)
        {
            effects.Add(effect);
        }
    }
}
