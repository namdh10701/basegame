using _Base.Scripts.Patterns.Observer;
using _Base.Scripts.Utils;

namespace _Game.Scripts.Test
{
    public class CurrencyManager : ObservedSingleton<CurrencyManager>
    {
        private int coin;
        public LerpManager CoinTextLerpManager;
        public LerpManager diamondTextLerpManager;
        public int Coin
        {
            get
            {
                return coin;
            }
            set
            {
                coin = value;
                Notify();
                CoinTextLerpManager.OnValueChanged(coin);
            }
        }
    }
}