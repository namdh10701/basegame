using _Game.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Features.Gameplay
{
    public class AmmoView : MonoBehaviour, IGridItemView
    {
        public static Color BrokenColor = new Color(.25f, .25f, .25f, 1);
        public static Color ActiveColor = Color.white;

        Ammo ammo;
        public AmmoHUD ammoHUD;
        public SpriteRenderer spriteRenderer;

        public void Init(IGridItem ammo)
        {
            this.ammo = ammo as Ammo;
            ammoHUD.SetAmmo(this.ammo);
        }

        public void HandleActive()
        {
            spriteRenderer.color = ActiveColor;
        }

        public void HandleBroken()
        {
            spriteRenderer.color = BrokenColor;
            spriteRenderer.color = BrokenColor;
        }
    }
}