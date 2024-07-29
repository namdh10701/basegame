using _Base.Scripts.Utils;
using _Game.Scripts.UI;
using Online;
using Online.Enum;
using UnityWeld.Binding;

namespace _Game.Scripts.Gameplay
{
    [Binding]
    public class StatusViewModel : RootViewModel
    {
        [Binding]
        public string GoldInfo => $"{PlayfabManager.Instance.Gold}";
        
        [Binding]
        public string GemInfo => $"{PlayfabManager.Instance.Gem}";
        
        [Binding]
        public string EnergyInfo => $"{PlayfabManager.Instance.Energy}";
        // public string EnergyInfo => $"{PlayfabManager.Instance.Gem}/{SaveSystem.GameSave.maxEnergy}";
        
        private async void Awake()
        {
            IOC.Register(this);
            
            PlayfabManager.Instance.Inventory.OnCurrencyChanged += InstanceOnOnCurrencyChanged;
        }

        private void OnDestroy()
        {
            PlayfabManager.Instance.Inventory.OnCurrencyChanged -= InstanceOnOnCurrencyChanged;
        }

        private void LoadCurrencies()
        {
            OnPropertyChanged(nameof(GoldInfo));
            OnPropertyChanged(nameof(GemInfo));
            OnPropertyChanged(nameof(EnergyInfo));
        }

        private void InstanceOnOnCurrencyChanged(EVirtualCurrency currency, int value)
        {
            LoadCurrencies();
        }

    }
}