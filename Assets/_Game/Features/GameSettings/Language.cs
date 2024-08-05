using System.Threading.Tasks;
using _Game.Features.Dialogs;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI;
using UnityWeld.Binding;

namespace _Game.Features.GameSettings
{
    [Binding]
    public class Language: SubViewModel
    {
        public string Id;
        
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

        #region Binding Prop: IsSelected

        /// <summary>
        /// IsSelected
        /// </summary>
        [Binding]
        public bool IsSelected => SaveSystem.GameSave.Settings.Language == Id;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [Binding]
        public async Task SetLanguage()
        {
            if (!await ConfirmModal.Show($"Change language to {Name} ?"))
            {
                return;
            }
            
            SaveSystem.GameSave.Settings.Language = Id;
            OnPropertyChanged(nameof(IsSelected));
        }

        public Language(string id, string name)
        {
            Id = id;
            _name = name;
        }
    }
}