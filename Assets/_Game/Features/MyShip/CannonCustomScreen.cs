using System;
using _Game.Features.Inventory;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;

namespace _Game.Features.InventoryCustomScreen
{
    [Binding]
    public class CannonCustomScreen : ModalWithViewModel
    {
        #region Binding: SkillInfoCannons
        private ObservableList<SkillInfoItem> skillInfoItems = new ObservableList<SkillInfoItem>();

        [Binding]
        public ObservableList<SkillInfoItem> SkillInfoItems => skillInfoItems;

        #endregion
        public static CannonCustomScreen Instance;

        public override async UniTask Initialize(Memory<object> args)
        {
            Instance = this;
            InitDataTest();
        }

        [Binding]
        public async void Close()
        {
        }

        private void InitDataTest()
        {
            for (int i = 0; i < 2; i++)
            {
                var SkillInfoItem = new SkillInfoItem();
                SkillInfoItem.Id = "0001";
                SkillInfoItem.SkillName = "Skill Nam 1";
                SkillInfoItem.Type = ItemType.CREW;
                SkillInfoItem.Details = "111111111111111111111111111111111111111111";
                skillInfoItems.Add(SkillInfoItem);
            }
        }
    }
}
