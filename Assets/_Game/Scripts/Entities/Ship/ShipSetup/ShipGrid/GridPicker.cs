using _Base.Scripts.Utils.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public enum CellPickType
    {
        ClosetCell, RandomCell
    }
    public class GridPicker : MonoBehaviour
    {
        public ShipSetup ShipGrid;

        public List<Cell> PickCells(Transform enemy, CellPickType pickType, CellPattern pattern, int size, out Cell centerCell)
        {
            List<Cell> cells = null;
            centerCell = null;
            switch (pickType)
            {
                case CellPickType.RandomCell:
                    centerCell = ShipGrid.AllCells.GetRandom();
                    cells = GridHelper.GetCellPattern(centerCell.Grid, pattern, centerCell, size);
                    break;
                case CellPickType.ClosetCell:
                    centerCell = GridHelper.GetClosetCellToPoint(ShipGrid.AllCells, enemy.position);
                    cells = GridHelper.GetCellPattern(centerCell.Grid, pattern, centerCell, size);
                    break;
            }
            return cells;
        }

        public List<Cell> PickCells(Transform enemy, AttackPatternProfile profile, out Cell centerCell)
        {
            return PickCells(enemy, profile.CellPickType, profile.CellPattern, profile.Size, out centerCell);
        }

    }
}
