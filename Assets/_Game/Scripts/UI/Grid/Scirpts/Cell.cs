
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace _Base.Scripts.UI
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image _backGround;
        [SerializeField] private TextMeshProUGUI _pos;
        private CellData _cellData;
        public void Setup(CellData cellData)
        {
            _cellData = cellData;
            _pos.text = $"{_cellData.position.x}:{_cellData.position.y}";
        }

        // public void CheckCellEmty()
        // {
        //     if (_cellData.statusCell == StatusCell.Empty)
        //         _backGround.color = Color.green;
        //     else
        //         _backGround.color = Color.red;
        // }

        public bool CheckCellEmty()
        {
            if (_cellData.statusCell == StatusCell.Empty)
                return true;
            return false;
        }

    }
}
