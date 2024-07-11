using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GWLPXL.InventoryGrid
{


    /// <summary>
    /// pretend item, used for the demo scene on buttons to create new pieces to add to inventory
    /// </summary>
    public class PretendItem : MonoBehaviour
    {
        public GridInventory_UI Grid;
        public PatternHolder PatternHolder;
        public int EquipID = -1;
        public void SetNewPiece()
        {
            Grid.CreateNewInventoryPiece(PatternHolder, EquipID);
        }
    }

}