using _Game.Scripts.Attributes;
using Slash.Unity.DataBind.Core.Data;

namespace _Game.Scripts
{
    public class RootContext: Context
    {
        public PlayerContext PlayerContext { get; set; } = new PlayerContext();
        
        #region Binding Prop: Score

        /// <summary>
        /// Score
        /// </summary>

        public int Score
        {
            get => scoreProperty.Value;
            set => scoreProperty.Value = value;
        }

        private readonly Property<int> scoreProperty = new(100);

        #endregion
    }
}