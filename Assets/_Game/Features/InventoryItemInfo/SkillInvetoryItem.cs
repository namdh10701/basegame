using System.Collections;
using System.Collections.Generic;
using _Game.Features.Inventory;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using UnityEngine;
using UnityWeld.Binding;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class SkillInvetoryItem : SubViewModel
    {
        #region Binding Prop: Name
        /// <summary>
        /// Name
        /// </summary>
        [Binding]
        public string Name
        {
            get => _name;
            set
            {
                if (Equals(_name, value))
                {
                    return;
                }

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _name;
        #endregion
        
        #region Binding Prop: OperationType
        /// <summary>
        /// OperationType
        /// </summary>
        [Binding]
        public string OperationType
        {
            get => _operationType;
            set
            {
                if (Equals(_operationType, value))
                {
                    return;
                }

                _operationType = value;
                OnPropertyChanged(nameof(OperationType));
            }
        }
        private string _operationType;
        #endregion

        #region Binding Prop: ItemType
        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType Type
        {
            get => m_type;
            set
            {
                if (Equals(m_type, value))
                {
                    return;
                }

                m_type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        private ItemType m_type;
        #endregion

        #region Binding Prop: Details
        /// <summary>
        /// Details
        /// </summary>
        [Binding]
        public string Details
        {
            get => _details;
            set
            {
                if (Equals(_details, value))
                {
                    return;
                }

                _details = value;
                OnPropertyChanged(nameof(Details));
            }
        }
        private string _details;
        #endregion

        [Binding]
        public Sprite Thumbnail
        {
            get
            {
                if (Type != null && OperationType != null)
                {
                    var itemType = Type.ToString().ToLower();
                    var name = OperationType.ToLower();
                    var path = $"Items/itemskill_{itemType}_{name}";
                    return Resources.Load<Sprite>(path);
                }
                else
                {
                    return Resources.Load<Sprite>($"Items/itemskill_crew_blonde");
                }

            }
        }

        public void Setup()
        {
            OnPropertyChanged(nameof(Thumbnail));
        }

    }
}
