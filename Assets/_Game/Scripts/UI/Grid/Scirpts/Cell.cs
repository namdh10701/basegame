using UnityEngine;
using UnityEngine.UI;
namespace _Base.Scripts.UI
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image _backGround;
        [SerializeField] private RectTransform _root;
        private CellData _cellData;
        public void Setup(CellData cellData)
        {
            _cellData = cellData;
            _root.sizeDelta = _cellData.size;
        }

        public void ChangeColor(Color color)
        {
            _backGround.color = color;

        }

        public bool CheckCellEmty()
        {
            if (_cellData.statusCell == StatusCell.Empty)
                return true;
            return false;
        }

        public void SetStatusCell(StatusCell statusCell)
        {
            _cellData.statusCell = statusCell;
        }

        public StatusCell GetStatusCell()
        {
            return _cellData.statusCell;
        }

        public Vector2 GetPosition()
        {
            return _cellData.position;
        }

        public CellData GetCellData()
        {
            return _cellData;
        }

    }
}
