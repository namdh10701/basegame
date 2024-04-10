
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public Color Color = Color.black;

    public void PointDragHandler(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = (PointerEventData)baseEventData;
        Image img = pointerEventData.pointerClick.gameObject.GetComponent<Image>();
        Color = img.color;
        // Color color;
        // RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData., canvas.worldCamera, out color);
        // transform.position = canvas.transform.TransformPoint(position);
    }
}
