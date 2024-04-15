using Demo.Scripts.Data;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public SpriteRenderer _spriteRenderer;
    public Vector2 Position;
    public Color _oldColor;
    public float _oldFade;
    public ItemType itemType;

    public bool HasItem;

    public void Setup(Vector2 position)
    {
        itemType = ItemType.None;
        Position = position;
        _oldColor = _spriteRenderer.color;
        _oldFade = _spriteRenderer.color.a;
    }

    public Vector2 GetBounds()
    {
        return new Vector2(_spriteRenderer.sprite.bounds.size.x, _spriteRenderer.sprite.bounds.size.y);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag != "DragObject")
            return;

        var col = Color.red;
        col.a = _oldFade;
        _spriteRenderer.color = col;
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag != "DragObject")
            return;
        Debug.LogWarning("OnTriggerExit" + collider2D.gameObject.name);
        var col = _oldColor;
        col.a = _oldFade;
        _spriteRenderer.color = col;
    }
}
