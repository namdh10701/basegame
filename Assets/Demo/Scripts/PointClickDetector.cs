using UnityEngine;

namespace Demo.Scripts
{
    public class PointClickDetector : MonoBehaviour, IPointClickDetector
    {
        [SerializeField] UnityEngine.Camera mainCamera;
        public System.Action<GameObject> OnClickCallback;
        private GameObject selectedObject;
        GameObject hitObject;
        private bool isDragging = false;

        void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    hitObject = hit.collider.gameObject;
                    switch (hitObject.tag)
                    {
                        case Helper.TAG_GUN_EMPLACEMENT:
                            GameController.Instance.ShowWeaponsMenu(hitObject);
                            break;
                        case Helper.TAG_WEAPON_ITEM:
                            GameController.Instance.OnSelectedWeaponItem(hitObject);
                            break;
                        case Helper.TAG_BULLET:
                            GameController.Instance.EnableBulletItem(hitObject, false);
                            selectedObject = GameController.Instance.SpawnBulletItem(hitObject);
                            isDragging = (selectedObject != null) ? true : false;
                            break;
                    }
                }
            }
            if (isDragging)
            {
                Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                selectedObject.transform.position = pos;
            }

            if (Input.GetMouseButtonUp(0) && hitObject.gameObject.tag == Helper.TAG_BULLET)
            {
                isDragging = false;
                GameController.Instance.EnableBulletItem(hitObject, true);
                Destroy(selectedObject.gameObject);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("GetMouseButtonUp" + hit.collider.gameObject.tag);
                    var canon = hit.collider.gameObject.GetComponent<GunEmplacement>().GetComponentInChildren<Canon.Canon>();
                    if (canon != null)
                    {
                        GameController.Instance.ReloadBullet(canon);
                        Debug.Log("canon" + canon.CanonData.Id);
                    }

                }
            }

        }
    }
}
