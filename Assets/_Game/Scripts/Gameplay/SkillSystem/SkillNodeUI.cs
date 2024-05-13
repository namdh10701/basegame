using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.SkillSystem
{
    public class SkillNodeUI : MonoBehaviour
    {
        SkillDefinition skillDefinition;
        public Image Image;
        public TextMeshProUGUI lvText;
        public Button BuySkillBtn;

        public void Init(SkillDefinition skillDefinition, int level)
        {
            this.skillDefinition = skillDefinition;
            Sprite sprite = ResourceLoader.LoadSkillImage(skillDefinition.Id);
            Image.sprite = sprite;
            lvText.text = $"Lv {level}";

        }

        private void OnEnable()
        {
            BuySkillBtn.onClick.AddListener(OnBuyClick);
        }

        private void OnDisable()
        {
            BuySkillBtn.onClick.RemoveListener(OnBuyClick);
        }

        void OnBuyClick()
        {
            Debug.Log("buy");
        }
    }
}