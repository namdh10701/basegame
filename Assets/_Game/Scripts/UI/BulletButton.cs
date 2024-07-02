using _Game.Scripts.Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BulletButton : MonoBehaviour
{
    public Image image;
    public Image selector;
    public Bullet bullet;
    public Button button;

    public UnityEvent onClick => button.onClick;
    public void Init(Bullet bullet)
    {
        this.bullet = bullet;
        image.sprite = bullet.Def.Image;
    }
}
