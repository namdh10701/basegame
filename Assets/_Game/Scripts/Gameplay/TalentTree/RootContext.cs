using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts.Gameplay.TalentTree
{
    public class RootContext: Context
    {
        #region Binding Prop: Records

        /// <summary>
        /// Records
        /// </summary>

        public Collection<RecordContext> Records
        {
            get => _recordsProperty.Value;
            set => _recordsProperty.Value = value;
        }

        private readonly Property<Collection<RecordContext>> _recordsProperty = new();

        #endregion
    }
}