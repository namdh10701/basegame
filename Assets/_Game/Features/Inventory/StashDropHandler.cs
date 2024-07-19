using _Base.Scripts.Utils;
using _Game.Features.MyShip;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class ItemDroppedCallbackCommand
    {
        public enum Command
        {
            REJECT,
            COMMIT
        }

        public Command Cmd;

        public object Data;
    }
    public class StashDropHandler : DropHandler
    {
        public override ItemDroppedCallbackCommand OnItemDrop(DraggableItem droppedItem)
        {
            
            var data = droppedItem.DragDataProvider.GetData<InventoryItem>();

            if (GetComponent<Template>().GetViewModel() is not MyShip.StashItem stashItem) return new ItemDroppedCallbackCommand { Cmd = ItemDroppedCallbackCommand.Command.REJECT };

            var beforeProcessData = stashItem.InventoryItem;

            var isSwitch = beforeProcessData != null;
            
            stashItem.InventoryItem = data;

            // if (isSwitch)
            // {
            //     (droppedItem.GetComponent<Template>().GetViewModel() as MyShip.StashItem).InventoryItem =
            //         beforeProcessData;
            // }
            // else
            // {
            //     (droppedItem.GetComponent<Template>().GetViewModel() as MyShip.StashItem).InventoryItem =
            //         null;
            // }
            
            return new ItemDroppedCallbackCommand { Cmd = ItemDroppedCallbackCommand.Command.COMMIT, Data = beforeProcessData };
        }
    }
}