using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.SeaMap
{
    public class SeaMapNode : SubViewModel
    {
        public enum NodeType
        {
            MiniBoss,
            Boss,
            Monster,
            MyShip,
            Treasure,
            Unknown
        }

        #region Binding Prop: Type

        /// <summary>
        /// Type
        /// </summary>
        [Binding]
        public NodeType Type
        {
            get => _type;
            set
            {
                if (Equals(_type, value))
                {
                    return;
                }

                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private NodeType _type;

        #endregion
        
        [Binding]
        public Sprite Thumbnail => Resources.Load<Sprite>($"Images/SeaMap/node_{Type.ToString()}");
    }
}
