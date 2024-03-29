using UnityEngine;

namespace Demo.Scripts.Canon
{
    public class CannonBulletManager : MonoBehaviour
    {
        [SerializeField] GameObject[] cannonBulletUIs;

        public int CurrentBullet = 6;
        public int maxBullet = 6;
        public void OnShoot()
        {
            CurrentBullet--;
            cannonBulletUIs[CurrentBullet].SetActive(false);
        }

        public void OnReload()
        {
            CurrentBullet = maxBullet;
            foreach (GameObject go in cannonBulletUIs)
            {
                go.SetActive(true);
            }
        }
    }
}
