using _Base.Scripts.RPG.Entities;
using _Game.Scripts;
using _Game.Scripts.Entities;
using Demo.Scripts.Data;
using UnityEngine;

namespace _Game.Scripts
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Vector2 _position;
        private Color _oldColor;
        private float _oldFade;
        private ItemType _itemType;
        private bool _isEmty = true;
        public int Id;
        public int X;
        public int Y;
        public bool HasItem;
        public Grid Grid;
        // TODO
        public GridItem GridItem;
        public void Setup(Vector2 position, int id)
        {
            _itemType = ItemType.None;
            _position = position;
            Id = id;
            _oldColor = _spriteRenderer.color;
            _oldFade = _spriteRenderer.color.a;
        }

        public Vector2 GetBounds()
        {
            return new Vector2(_spriteRenderer.sprite.bounds.size.x, _spriteRenderer.sprite.bounds.size.y);
        }

        public void SetItemType(ItemType itemType)
        {
            _itemType = itemType;
        }

        public ItemType GetItemType()
        {
            return _itemType;
        }

        public Vector2 GetPositionCell()
        {
            return _position;
        }

        public void EnableCell(bool hasItem)
        {
            if (!hasItem)
                _spriteRenderer.enabled = false;
            else
            {
                _spriteRenderer.enabled = true;
                var col = _oldColor;
                col.a = _oldFade;
                _spriteRenderer.color = col;
            }

        }

        public void OnChangeColorCell(bool isChange)
        {
            if (isChange)
            {
                var col = Color.red;
                col.a = _oldFade;
                _spriteRenderer.color = col;
            }
            else
            {
                var col = _oldColor;
                col.a = _oldFade;
                _spriteRenderer.color = col;
            }
        }

        public bool IsCellEmty()
        {
            return _isEmty;
        }

        public void CheckCellsEmty(bool isEmty)
        {
            _isEmty = isEmty;
        }


    }
}