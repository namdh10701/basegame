using System;
using System.Linq;
using _Game.Features.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Modals;

namespace _Game.Features.InventoryItemInfo
{
    [Binding]
    public class InventoryItemInfoModal : ModalWithViewModel
    {
        [SerializeField] GameObject _enhance;
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

        #region Binding Prop: Skill
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Skill_1
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

        #region Binding Prop: Skill_2
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Skill_2
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

        #region Binding Prop: Skill_3
        /// <summary>
        /// Thumbnail
        /// </summary>
        [Binding]
        public Sprite Skill_3
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

        public override async UniTask Initialize(Memory<object> args)
        {
            InventoryItem = args.ToArray().FirstOrDefault() as InventoryItem;
        }

        [Binding]
        public async void Close()
        {
            await ModalContainer.Find(ContainerKey.Modals).PopAsync(true);
        }


        // public override UniTask WillPushEnter(Memory<object> args)
        // {
        //     var receivedObj = args.ToArray().FirstOrDefault();
        //     if (receivedObj is not InventoryItem item)
        //     {
        //         return UniTask.CompletedTask;
        //     }

        //     InventoryItem = item;
        //     //
        //     // if (item.Type == ItemType.CANNON)
        //     // {
        //     //     var cannon = GDConfigLoader.Instance.Cannons[item.Id];
        //     // } 
        //     // else if (item.Type == ItemType.AMMO)
        //     // {
        //     //     var ammo = GDConfigLoader.Instance.Ammos[item.Id];
        //     // }
        //     // else if (item.Type == ItemType.CREW)
        //     // {
        //     //     var crew = GDConfigLoader.Instance.Ammos[item.Id];
        //     // }
        //     _enhance.SetActive((m_type == ItemType.CREW) ? true : false);
        //     return UniTask.CompletedTask;
        // }
    }
}