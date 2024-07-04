using System;
using _Game.Scripts.Gameplay.TalentTree;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.TalentTree
{
    [Binding]
    public class TalentTreeScreen : ScreenWithViewModel
    {
        [Binding]
        public ObservableList<LevelRecord> Items { get; set; } = new ();
        
        public override async UniTask Initialize(Memory<object> args)
        {
            // await UniTask.Delay(TimeSpan.FromSeconds(1));
            InitData();
        }
        
        private void InitData()
        {
            // var maxLevel = GDConfigLoader.Instance.TalentTreeNormals.Max(v => v.Value.main);
            // for (int lvl = 0; lvl < maxLevel + 1; lvl++)
            // {
            //     var normalItems = GDConfigLoader.Instance.TalentTreeNormals.Where(v => v.Value.main.ToString() == lvl.ToString()).ToList();
            //     var preItem = GDConfigLoader.Instance.TalentTreePres.FirstOrDefault(v => v.Value.premium.ToString() == lvl.ToString()).Value;
            //
            //     var normalItemsCount = normalItems.Count();
            //     for (int j = 0; j < normalItemsCount; j++)
            //     {
            //         var isLast = j == normalItemsCount - 1;
            //         var normalItem = normalItems[j];
            //     
            //         Items.Add(new LevelRecord
            //         {
            //             NormalNode = new NodeViewModel
            //             {
            //                 ItemId = normalItem.Value.stat_id
            //             },
            //             LevelNode = new NodeViewModel
            //             {
            //                 CanvasGroupAlpha = isLast ? 1: 0, 
            //                 Level = lvl
            //             },
            //             PremiumNode = new NodeViewModel
            //             {
            //                 CanvasGroupAlpha = (isLast && preItem != null) ? 1 : 0,
            //                 ItemId = preItem == null ? "" : preItem.stat_id
            //             },
            //         });
            //     }
            // }

        }

        #region Method Binding

        /// <summary>
        /// Nav back
        /// </summary>
        [Binding]
        public async void NavBack()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }

        #endregion
    }
}
