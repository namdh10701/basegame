using _Game.Features.Gameplay;
using _Game.Scripts.DB;
using _Game.Scripts.Entities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmmoButton : MonoBehaviour
{
    public Image image;
    public Image selector;
    public Ammo ammo;
    public TextMeshProUGUI manaText;
    public void Init(Ammo ammo)
    {
        AmmoStats ammoStats = ammo.Stats as AmmoStats;
        this.ammo = ammo;
        image.sprite = Database.GetAmmoImage(ammo.Id);
        manaText.text = ammoStats.EnergyCost.Value.ToString();
    }

    public void ToggleSelect(bool isOn)
    {
        selector.gameObject.SetActive(isOn);
    }

}
