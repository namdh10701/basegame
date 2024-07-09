using System;
using System.Linq;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class EnhanceItemInventoryModal : ModalWithViewModel
    {
        [Binding]
        public InventoryItem InventoryItem { get; set; }

        [Binding]
        public string Id { get; set; }


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

        #region Binding Prop: SpriteMainItem
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite SpriteMainItem
        {
            get
            {
                switch (Type)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(Id);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(Id);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(Id);
                    default:
                        return null;
                }
            }
        }
        #endregion

        #region Binding Prop: Ingredients
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Ingredients
        {
            get
            {
                switch (Type)
                {
                    case ItemType.CANNON:
                        return _Game.Scripts.DB.Database.GetCannonImage(Id);
                    case ItemType.CREW:
                        return _Game.Scripts.DB.Database.GetCrewImage(Id);
                    case ItemType.AMMO:
                        return _Game.Scripts.DB.Database.GetAmmoImage(Id);
                    default:
                        return null;
                }
            }
        }
        #endregion

        #region Binding Prop: ItemType

        /// <summary>
        /// ItemType
        /// </summary>
        [Binding]
        public ItemType IngredientsType
        {
            get => m_IngredientsType;
            set
            {
                if (Equals(m_IngredientsType, value))
                {
                    return;
                }

                m_IngredientsType = value;
                OnPropertyChanged(nameof(IngredientsType));
            }
        }

        private ItemType m_IngredientsType;

        #endregion

        #region Binding Prop: GoldUser

        /// <summary>
        /// GoldUser
        /// </summary>
        [Binding]
        public string GoldUser
        {
            get => m_GoldUser;
            set
            {
                if (Equals(m_GoldUser, value))
                {
                    return;
                }

                m_GoldUser = value;
                OnPropertyChanged(nameof(GoldUser));
            }
        }

        private string m_GoldUser;
        #endregion

        #region Binding Prop: GoldEnhance
        /// <summary>
        /// GoldEnhance
        /// </summary>
        [Binding]
        public string GoldEnhance
        {
            get => m_GoldEnhance;
            set
            {
                if (Equals(m_GoldEnhance, value))
                {
                    return;
                }

                m_GoldEnhance = value;
                OnPropertyChanged(nameof(GoldEnhance));
            }
        }

        private string m_GoldEnhance;
        #endregion

        #region Binding Prop: IngredientUser

        /// <summary>
        /// IngredientUser
        /// </summary>
        [Binding]
        public string IngredientUser
        {
            get => m_IngredientUser;
            set
            {
                if (Equals(m_IngredientUser, value))
                {
                    return;
                }

                m_IngredientUser = value;
                OnPropertyChanged(nameof(IngredientUser));
            }
        }

        private string m_IngredientUser;
        #endregion

        #region Binding Prop: IngredientEnhance

        /// <summary>
        /// IngredientEnhance
        /// </summary>
        [Binding]
        public string IngredientEnhance
        {
            get => m_IngredientEnhance;
            set
            {
                if (Equals(m_IngredientEnhance, value))
                {
                    return;
                }

                m_IngredientEnhance = value;
                OnPropertyChanged(nameof(IngredientEnhance));
            }
        }

        private string m_IngredientEnhance;
        #endregion

        #region Binding Prop: PreviousLevel

        /// <summary>
        /// PreviousLevel
        /// </summary>
        [Binding]
        public string PreviousLevel
        {
            get => m_PreviousLevel;
            set
            {
                if (Equals(m_PreviousLevel, value))
                {
                    return;
                }

                m_PreviousLevel = value;
                OnPropertyChanged(nameof(PreviousLevel));
            }
        }

        private string m_PreviousLevel;
        #endregion


        #region Binding Prop: PreviousLevel

        /// <summary>
        /// PreviousLevel
        /// </summary>
        [Binding]
        public string NextLevel
        {
            get => m_NextLevel;
            set
            {
                if (Equals(m_NextLevel, value))
                {
                    return;
                }

                m_NextLevel = value;
                OnPropertyChanged(nameof(NextLevel));
            }
        }

        private string m_NextLevel;
        #endregion

        public override UniTask WillPushEnter(Memory<object> args)
        {
            var receivedObj = args.ToArray().FirstOrDefault();
            if (receivedObj is not InventoryItem item)
            {
                return UniTask.CompletedTask;
            }

            InventoryItem = item;
            //
            // if (item.Type == ItemType.CANNON)
            // {
            //     var cannon = GDConfigLoader.Instance.Cannons[item.Id];
            // } 
            // else if (item.Type == ItemType.AMMO)
            // {
            //     var ammo = GDConfigLoader.Instance.Ammos[item.Id];
            // }
            // else if (item.Type == ItemType.CREW)
            // {
            //     var crew = GDConfigLoader.Instance.Ammos[item.Id];
            // }
            return UniTask.CompletedTask;
        }
    }
}