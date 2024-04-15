using UnityEditor;
using UnityEngine;

public class PointClickDetector : MonoBehaviour
{
    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private GameObject _gameObjectSlected;
    public void Update()
    {
        if (_isDragActive)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
                return;

            }
        }
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;
            _screenPosition = new Vector2(mousePosition.x, mousePosition.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
            return;

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "DragObject")
            {
                _gameObjectSlected = hit.collider.gameObject;

                InitDrag();
            }
        }

    }
    private void InitDrag()
    {
        _isDragActive = true;
    }

    private void Drag()
    {
        _gameObjectSlected.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    private void Drop()
    {
        Debug.Log("Drop" + _gameObjectSlected.name);
        if (_gameObjectSlected == null)
            return;

        var dragItem = _gameObjectSlected.GetComponent<DragItem>();
        dragItem.GetCellSelect(dragItem.ItemMenuData);
        Destroy(_gameObjectSlected);
        _isDragActive = false;



    }
}

