using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace _Game.Features.Ranking
{
    [RequireComponent(typeof(Button))]
    public class RankingItemRewardButtonController: MonoBehaviour
    {
        private RankingScreen _rankingScreen;
        public Template Template;
        private RankingScreen.RankRecord _rankRecord;
        private Button _button;

        private void Awake()
        {
            _rankingScreen = GetComponentInParent<RankingScreen>();
            _button = GetComponent<Button>();
            _rankRecord = (RankingScreen.RankRecord)Template.GetViewModel();
            
            _button.onClick.AddListener(OnBtnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnBtnClick);
        }

        private void OnBtnClick()
        {
            _rankingScreen.FocusedRankRecord = _rankRecord;

            var rect = ((RectTransform)transform);
            var pos = new Vector3(rect.position.x, rect.position.y);
            pos.x += rect.rect.width / 2;
            pos.y += rect.rect.height / 2;
            _rankingScreen.FocusedRankRewardBubblePos = pos;
        }
    }
}
