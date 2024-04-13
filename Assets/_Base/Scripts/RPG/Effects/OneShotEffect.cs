namespace _Base.Scripts.RPG.Effects
{
    /// <summary>
    /// Apply effect then destroy immediately
    /// </summary>
    public abstract class OneShotEffect: Effect
    {
        public override void Process()
        {
            OnBeforeApply();
            Apply();
            OnAfterApply();
            Destroy(this);
        }
    }
}