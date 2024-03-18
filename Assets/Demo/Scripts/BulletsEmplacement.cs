using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsEmplacement : MonoBehaviour
{
    [SerializeField] SpriteRenderer _bullet;
    public int ID;
    public void Setup(int id)
    {
        ID = id;
    }

    public void EnableItem(bool enable)
    {
        _bullet.gameObject.SetActive(enable);
    }
}
