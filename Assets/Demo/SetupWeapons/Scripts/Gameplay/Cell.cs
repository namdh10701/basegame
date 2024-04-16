using Demo.Scripts.Data;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector2 _position;
    private Color _oldColor;
    private float _oldFade;
    private ItemType _itemType;
    private bool _isEmty = true;

    public bool HasItem;

    public void Setup(Vector2 position)
    {
        _itemType = ItemType.None;
        _position = position;
        _oldColor = _spriteRenderer.color;
        _oldFade = _spriteRenderer.color.a;
    }

    public Vector2 GetBounds()
    {
        return new Vector2(_spriteRenderer.sprite.bounds.size.x, _spriteRenderer.sprite.bounds.size.y);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!_isEmty)
            return;
        if (collider2D.gameObject.tag == "DragObject")
        {
            var col = Color.red;
            col.a = _oldFade;
            _spriteRenderer.color = col;
        }
        else if (collider2D.gameObject.tag == "WeaponItem")
        {
            EnableCell(false);
        }


    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {

        if (collider2D.gameObject.tag == "DragObject")
        {
            var col = _oldColor;
            col.a = _oldFade;
            _spriteRenderer.color = col;
        }
        else if (collider2D.gameObject.tag == "WeaponItem")
        {
            EnableCell(true);

        }


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
        _isEmty = hasItem;
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

    public bool IsCellEmty()
    {
        return _isEmty;
    }


}
