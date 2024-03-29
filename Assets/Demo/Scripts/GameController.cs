using System.Collections;
using System.Collections.Generic;
using _Base.Scripts.Utils;
using Demo.Scripts.Data;
using DG.Tweening;
using UnityEngine;

namespace Demo.Scripts
{
    public class GameController : SingletonMonoBehaviour<GameController>
    {
        [SerializeField] ShipController _ship;
        [SerializeField] WeaponsMenu _prefabWeaponsMenu;
        [SerializeField] WeaponsMennuConfig _weaponsMenuConfig;
        [SerializeField] BulletsConfig _bulletsConfig;
        [SerializeField] List<Canon.Canon> canons;
        [SerializeField] EnemyController _enemyController;


        WeaponsMenu _weaponsMenu;

        void Awake()
        {
            Initializa();
        }

        private void Initializa()
        {
            _ship.Setup(_bulletsConfig);
            StartCoroutine(SpawnEnemies());
        }

        public void ShowWeaponsMenu(GameObject go)
        {
            _ship.RemoveCanon(go.GetComponent<GunEmplacement>().Id);
            if (_weaponsMenu != null)
            {
                Destroy(_weaponsMenu.gameObject);
            }
            _weaponsMenu = Instantiate(_prefabWeaponsMenu, go.transform);
            _weaponsMenu.transform.localPosition = new Vector3(0, 0, 0);
            _weaponsMenu.transform.DOScale(new Vector3(2.0f, 2.0f, 0f), 0.2f);
            _weaponsMenu.SetUp(_weaponsMenuConfig, go);


        }

        public void OnSelectedWeaponItem(GameObject go)
        {
            var weaponItem = go.gameObject.GetComponent<WeaponItem>();

            _weaponsMenu.transform.DOScale(new Vector3(1.0f, 1.0f, 0f), 0.2f).OnComplete(() =>
            {
                Destroy(_weaponsMenu.gameObject);
                SpawnCanon(weaponItem.GetDataWeaponItem());
            });
        }

        public void SpawnCanon(WeaponItem item)
        {
            foreach (var canon in canons)
            {
                var gunEmplacement = item.Parent.gameObject.GetComponent<GunEmplacement>();

                if (canon.CanonData.Id == item.WeaponData.Id)
                {

                    var temp = Instantiate(canon, item.Parent.transform);
                    if (gunEmplacement.Id > 2)
                    {
                        //temp.transform.localEulerAngles = new Vector3(0, 0, 0);
                        temp.GetComponent<Canon.Canon>().Visual.transform.localEulerAngles = new Vector3(0, 0, 180);
                        temp.GetComponent<Canon.Canon>().AttackTrigger.transform.localEulerAngles = new Vector3(0, 0, 180);
                    }
                    gunEmplacement.AddCanon(temp);
                    gunEmplacement.SetWeaponData(item.WeaponData);

                }
            }
        }

        public GameObject SpawnBulletItem(GameObject parent)
        {
            return _ship.SpawnBulletItem(parent);
        }

        public void EnableBulletItem(GameObject go, bool enable)
        {
            _ship.EnableBulletItem(go.GetComponent<BulletsEmplacement>().BulletData.Id, enable);
        }

        public void ReloadBullet(Canon.Canon canon)
        {
            _ship.ReloadBullet(canon);
        }

        public IEnumerator SpawnEnemies()
        {
            yield return new WaitForSeconds(10.0f);
        }

        public void Spawn()
        {
            _enemyController.SpawnEnemy();

        }
    }
}