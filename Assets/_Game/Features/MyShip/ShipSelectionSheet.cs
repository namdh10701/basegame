using _Game.Features.Home;
using UnityWeld.Binding;
using ZBase.UnityScreenNavigator.Core.Sheets;

namespace _Game.Features.MyShip
{
    [Binding]
    public class ShipSelectionSheet : SheetWithViewModel
    {
        public class ShipSelectionSheetOutputData : MainShipSheet.InputData<string>
        {
            public ShipSelectionSheetOutputData(string data) : base(data)
            {
            }
        }
        
        [Binding]
        public async void NavBack()
        {
            var selectedShipId = GetSelectedShipId();
            
            var output = new ShipSelectionSheetOutputData(selectedShipId);
            await MyShipScreen.Instance.ShowSheet(Sheets.MainShipSheet, output);
        }

        private string GetSelectedShipId()
        {
            return "0001";
        }
    }
}