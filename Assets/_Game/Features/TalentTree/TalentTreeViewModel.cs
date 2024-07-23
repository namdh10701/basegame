using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using _Game.Features;
using _Game.Scripts.GD;
using _Game.Scripts.GD.DataManager;
using UnityEngine;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Scripts.Gameplay.TalentTree
{
    [Binding]
    public class TalentTreeViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private ObservableList<LevelRecord> items = new ObservableList<LevelRecord>()
        {
            
        };

        [Binding]
        public ObservableList<LevelRecord> Items
        {
            get
            {
                return items;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        private void Awake()
        {
            // GDConfigLoader.Instance.OnLoaded += Init;
            // GDConfigLoader.Instance.Load();
        }

        private void Init()
        {
            var maxLevel = GameData.TalentTreeNormalTable.Records.Max(v => v.Level);
            for (int lvl = 0; lvl < maxLevel + 1; lvl++)
            {
                var normalItems = GDConfigLoader.Instance.TalentTreeNormals.Where(v => v.Value.main.ToString() == lvl.ToString()).ToList();
                var preItem = GDConfigLoader.Instance.TalentTreePres.FirstOrDefault(v => v.Value.premium.ToString() == lvl.ToString()).Value;

                var normalItemsCount = normalItems.Count();
                for (int j = 0; j < normalItemsCount; j++)
                {
                    var isLast = j == normalItemsCount - 1;
                    var normalItem = normalItems[j];
                
                    items.Add(new LevelRecord
                    {
                        NormalNode = new NodeViewModel
                        {
                            ItemId = normalItem.Value.stat_id
                        },
                        LevelNode = new NodeViewModel
                        {
                            CanvasGroupAlpha = isLast ? 1: 0, 
                            Level = lvl
                        },
                        PremiumNode = new NodeViewModel
                        {
                            CanvasGroupAlpha = (isLast && preItem != null) ? 1 : 0,
                            ItemId = preItem == null ? "" : preItem.stat_id
                        },
                    });
                }
            }

        }
        
        [Binding]
        public async void NavBack()
        {
            await ScreenContainer.Find(ContainerKey.Screens).PopAsync(true);
        }
    }
}