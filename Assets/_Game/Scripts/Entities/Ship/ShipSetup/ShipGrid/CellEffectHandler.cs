using _Base.Scripts.RPG.Effects;
using _Game.Features.Gameplay;

public class CellEffectHandler : EffectHandler
{
    public Cell cell;
    public override void Apply(Effect effect)
    {

        effect = Instantiate(effect, null);
        effect.enabled = true;
        effect.transform.position = transform.position;


        if (cell.GridItem != null)
        {
            IEffectTaker taker = cell.GridItem as IEffectTaker;
            taker.EffectHandler.Apply(effect);
            return;
        }

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
        effect.Apply(cell.Grid.ship);

        if (effect is TimeoutEffect)
        {
            effects.Add(effect);
        }
    }
}
