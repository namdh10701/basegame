using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    private Vector2 _position;
    public Color _oldColor;
    public float _oldFade;

    public void Setup(Vector2 position)
    {
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
        if (collider2D.gameObject.tag != "DragObject")
            return; 

        Debug.LogWarning("OnTriggerEnter" + collider2D.gameObject.name);
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
