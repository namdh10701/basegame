using _Game.Features.Gameplay;
using _Game.Scripts.DB;
using _Game.Scripts.Entities;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmmoButton : MonoBehaviour
{
    public ShipHUD hud;
    public Image image;
    public Image selector;
    public Ammo ammo;
    public Button button;

    public UnityEvent onClick => button.onClick;
    public void Init(Ammo ammo)
    {
        this.ammo = ammo;
        image.sprite = Database.GetAmmoImage(ammo.Id);
    }

    public void ToggleSelect(bool isOn)
    {
        selector.gameObject.SetActive(isOn);
    }

    public void OnClick()
    {
        hud.OnClick(ammo);
    }

}
