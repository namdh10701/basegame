namespace _Base.Scripts.RPG.Effects
{
    /// <summary>
    /// Apply effect then destroy immediately
    /// </summary>
    public abstract class OneShotEffect: Effect
    {
        private void Start()
        {
            OnBeforeApply();
            Apply();
            OnAfterApply();
            Destroy(this);
        }
    }
}