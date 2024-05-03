using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.SkillSystem
{
    public class SkillNode : MonoBehaviour
    {
        public int Id;
        public List<SkillNode> Outgoing;
        public List<Image> OutgoingLine;
        public Image Image;
        public Button BuySkillBtn;
        public Action<SkillNode> OnClickAction;

        public Color boughtColor;
        public Color disabledColor;
        public Color enabledColor;

        private void Awake()
        {
            Sprite sprite = ResourceLoader.LoadSkillImage(Id);
            Image.sprite = sprite;
        }

        private void OnEnable()
        {
            BuySkillBtn.onClick.AddListener(OnBuySkillClick);
        }

        private void OnDisable()
        {
            BuySkillBtn.onClick.RemoveListener(OnBuySkillClick);
        }

        void OnBuySkillClick()
        {
            OnClickAction?.Invoke(this);
        }

        public void Disable()
        {

        }

        public void Enable()
        {

        }

        public void EnableOutgoings()
        {

        }
    }
}
