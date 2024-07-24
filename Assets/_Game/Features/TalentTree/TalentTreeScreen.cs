using System;
using _Game.Scripts.Gameplay.TalentTree;
using _Game.Scripts.GD.DataManager;
using _Game.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Features.TalentTree
{
    [Binding]
    public class TalentTreeScreen : ScreenWithViewModel
    {
        [Binding]
        public class TalentNode: SubViewModel
        {
            #region Binding Prop: IsActive

            /// <summary>
            /// IsActive
            /// </summary>
            [Binding]
            public bool IsActive
            {
                get => _isActive;
                set
                {
                    if (Equals(_isActive, value))
                    {
                        return;
                    }

                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }

            private bool _isActive;

            #endregion

            #region Binding Prop: Content

            /// <summary>
            /// Content
            /// </summary>
            [Binding]
            public string Content
            {
                get => _content;
                set
                {
                    if (Equals(_content, value))
                    {
                        return;
                    }

                    _content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }

            private string _content;

            #endregion

            #region Binding Prop: Value

            /// <summary>
            /// Value
            /// </summary>
            [Binding]
            public string Value
            {
                get => _value;
                set
                {
                    if (Equals(_value, value))
                    {
                        return;
                    }

                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }

            private string _value;

            #endregion

            #region Binding Prop: IsShow

            /// <summary>
            /// IsShow
            /// </summary>
            [Binding]
            public bool IsShow => !string.IsNullOrEmpty(Value);

            #endregion
        }

        [Binding]
        public class LevelNode : SubViewModel
        {
            #region Binding Prop: Title

            /// <summary>
            /// Title
            /// </summary>
            [Binding]
            public string Title
            {
                get => _title;
                set
                {
                    if (Equals(_title, value))
                    {
                        return;
                    }

                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }

            private string _title;

            #endregion
            
            #region Binding Prop: IsShow

            /// <summary>
            /// IsShow
            /// </summary>
            [Binding]
            public bool IsShow => !string.IsNullOrEmpty(Title);

            #endregion
        }
        
        [Binding]
        public ObservableList<TalentNode> NormalTalentNodes { get; } = new ();
        
        [Binding]
        public ObservableList<TalentNode> PremiumTalentNodes { get; } = new ();
        
        [Binding]
        public ObservableList<LevelNode> LevelNodes { get; } = new ();
        
        public override async UniTask Initialize(Memory<object> args)
        {
            // await UniTask.Delay(TimeSpan.FromSeconds(1));
            InitData();
        }
        
        private void InitData()
        {
            var lv = -1;
            foreach (var normalNodeRec in GameData.TalentTreeNormalTable.Records)
            {
                var normalItem = GameData.TalentTreeItemTable.FindById(normalNodeRec.ItemId);
                var info = normalItem?.GetInfo();

                if (info != null)
                {
                    NormalTalentNodes.Add(new TalentNode()
                    {
                        Value = $"+{info.Value}",
                        Content = info.Name,
                        IsActive = true,
                    });
                }

                if (normalNodeRec.Level != lv)
                {
                    lv = normalNodeRec.Level;
                    
                    // pre
                    var preNodeRec = GameData.TalentTreePremiumTable.FindByLevel(lv);
                    if (preNodeRec == null)
                    {
                        PremiumTalentNodes.Add(new TalentNode());
                        LevelNodes.Add(new LevelNode());
                        continue;
                    }
                
                    var preItem = GameData.TalentTreeItemTable.FindById(preNodeRec.ItemId);
                    var preInfo = preItem?.GetInfo();
                    if (preInfo != null)
                    {
                        PremiumTalentNodes.Add(new TalentNode()
                        {
                            Value = $"+{preInfo.Value}",
                            Content = preInfo.Name,
                            IsActive = true,
                        });
                    
                        // title
                        LevelNodes.Add(new LevelNode()
                        {
                            Title = $"Level {lv}",
                        });
                    }
                }
                else
                {
                    PremiumTalentNodes.Add(new TalentNode());
                    LevelNodes.Add(new LevelNode());
                }
            }
            //
            // foreach (var node in GameData.TalentTreePremiumTable.Records)
            // {
            //     var nodeLv = GameData.TalentTreeNormalTable.GetLevelRecordCount(node.Level);
            //     for (int i = 0; i < nodeLv; i++)
            //     {
            //         PremiumTalentNodes.Add(new TalentNode());
            //     }
            //     
            //     var item = GameData.TalentTreeItemTable.FindById(node.ItemId);
            //     if (item == null) continue;
            //     
            //     var info = item.GetInfo();
            //     if (info == null) continue;
            //     
            //     PremiumTalentNodes.Add(new TalentNode()
            //     {
            //         Value = info.Value,
            //         Content = info.Name,
            //         IsActive = true,
            //     });
            // }
            
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
