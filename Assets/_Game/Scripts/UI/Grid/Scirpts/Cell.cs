
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Base.Scripts.UI
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image _backGround;
        [SerializeField] private TextMeshProUGUI _pos;
        [SerializeField] private RectTransform _root;
        private CellData _cellData;
        public void Setup(CellData cellData)
        {
            _cellData = cellData;
            _pos.text = $"{_cellData.r}:{_cellData.c}";
            _root.sizeDelta = _cellData.size;
        }

        public void ChangeColor()
        {
            if (_cellData.statusCell == StatusCell.Empty)
                _backGround.color = Color.red;
            else
                _backGround.color = Color.green;
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
