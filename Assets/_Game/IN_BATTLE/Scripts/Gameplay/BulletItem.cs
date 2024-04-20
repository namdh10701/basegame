using Demo.Scripts.Data;
using UnityEngine;
public class BulletItem : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    public int _id;
    public void Setup(int id, SpriteRenderer spriteRenderer)
    {
        _id = id;
        _spriteRenderer = spriteRenderer;
    }

    public int GetId()
    {
        return _id;
    }
}

