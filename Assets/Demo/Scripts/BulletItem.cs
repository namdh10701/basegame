using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    BulletData _bulletsData;
    public void Setup(BulletData bulletData)
    {
        _bulletsData = bulletData;
        _spriteRenderer.sprite = bulletData.Sprite;
    }
}
