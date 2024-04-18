using System.Collections.Generic;
using _Base.Scripts.RPG.Entities;
using _Base.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Gameplay.Ship
{
    public class Ship : Entity//SingletonMonoBehaviour<Ship>
    {
        public static Ship Instance;
        // public ShipMana ShipMana;
        [SerializeField]
        private ShipStats stats = new();
        public override Stats Stats => stats;

        [Header("Config Data")]
        [SerializeField] DataShips _dataShips;
        [SerializeField] List<Transform> PositionGrids = new List<Transform>();
        [SerializeField] WeaponItem _prefabWeaponItem;

        private Dictionary<string, List<Cell>> _gridsInfor = new Dictionary<string, List<Cell>>();
        public List<WeaponItem> _weaponItems = new List<WeaponItem>();

        public TypeShip _curentSkin = TypeShip.Normal;
        public ShipConfig _curentShip;
        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        private void Start()
        {
            GetPositionGrids();
            LoadWeaponItems();
        }

        private void Update()
        {
            if (!stats.ManaPoint.IsFull)
            {
                stats.ManaPoint.Value += (stats.ManaRegenerationRate.Value * Time.deltaTime);
            }

            if (!stats.HealthPoint.IsFull)
            {
                stats.HealthPoint.Value += (stats.HealthRegenerationRate.Value * Time.deltaTime);
            }
        }

        private void GetPositionGrids()
        {
            foreach (var ship in _dataShips.ships)
            {
                ship.typeShip = _curentSkin;
                if (_curentSkin == ship.typeShip)
                {
                    _curentShip = ship;
                    for (int i = 0; i < PositionGrids.Count; i++)
                    {
                        ship.grids[i].transform = PositionGrids[i].transform;
                        var grid = PositionGrids[i].GetComponent<Grid>();
                        grid.Setup(ship.grids[i]);
                    }
                    CreateGrids(ship);
                }

            }

        }

        private void CreateGrids(ShipConfig shipConfig)
        {
            var idCell = 0;
            foreach (var grid in shipConfig.grids)
            {
                var listCell = new List<Cell>();
                for (int i = 0; i < grid.rows; i++)
                {
                    for (int j = 0; j < grid.cols; j++)
                    {
                        var go = Instantiate(shipConfig.cell, grid.transform);
                        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        var cell = go.GetComponent<Cell>();
                        var size = cell.GetBounds();
                        cell.Setup(new Vector2(i * size.x / 2, j * size.y / 2), idCell);
                        go.transform.localPosition = new Vector2(i * size.x / 2, j * size.y / 2);
                        listCell.Add(cell);
                        idCell++;
                    }
                }
                _gridsInfor.Add(grid.id, listCell);
            }

        }

        public void LoadWeaponItems()
        {
            foreach (var grid in _curentShip.grids)
            {
                foreach (var ship in _dataShips.ships)
                {
                    foreach (var weaponItemData in ship.weaponItemDatas)
                    {
                        if (weaponItemData.previousGridID == grid.id)
                        {
                            var itemWeapon = Instantiate(_prefabWeaponItem, grid.transform);
                            itemWeapon.transform.localPosition = weaponItemData.previousPosition;
                            itemWeapon.Setup(weaponItemData);
                            _weaponItems.Add(itemWeapon);
                        }
                    }
                }

            }
        }

    }
}
