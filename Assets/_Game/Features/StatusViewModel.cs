using System.Threading.Tasks;
using _Base.Scripts.Utils;
using _Game.Features.Home;
using _Game.Features.MyShipScreen;
using _Game.Scripts.DB;
using _Game.Scripts.GD;
using _Game.Scripts.SaveLoad;
using _Game.Scripts.UI;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Screens;

namespace _Game.Scripts.Gameplay
{
    [Binding]
    public class StatusViewModel : RootViewModel
    {
        #region Binding Prop: Gold

        /// <summary>
        /// Gold
        /// </summary>
        [Binding]
        public int Gold
        {
            get => _gold;
            set
            {
                if (Equals(_gold, value))
                {
                    return;
                }

                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        private int _gold;

        #endregion

        #region Binding Prop: Gem

        /// <summary>
        /// Gem
        /// </summary>
        [Binding]
        public int Gem
        {
            get => _gem;
            set
            {
                if (Equals(_gem, value))
                {
                    return;
                }

                _gem = value;
                OnPropertyChanged(nameof(Gem));
            }
        }

        private int _gem;

        #endregion

        #region Binding Prop: Energy

        /// <summary>
        /// Energy
        /// </summary>
        [Binding]
        public int Energy
        {
            get => _energy;
            set
            {
                if (Equals(_energy, value))
                {
                    return;
                }

                _energy = value;
                OnPropertyChanged(nameof(Energy));
            }
        }

        private int _energy;

        #endregion

        [Binding]
        public string GoldInfo => $"{SaveSystem.GameSave.gold}";
        
        [Binding]
        public string GemInfo => $"{SaveSystem.GameSave.gem}";
        
        [Binding]
        public string EnergyInfo => $"{SaveSystem.GameSave.energy}/{SaveSystem.GameSave.maxEnergy}";

        private async void Awake()
        {
            IOC.Register(this);
        }

    }
}