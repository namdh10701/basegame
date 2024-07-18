using UnityEngine;
using UnityEngine.UI;

public class ScrollRectController : MonoBehaviour
{
    public ScrollRect scrollRect;  // Tham chiếu đến ScrollRect
    public Button nextButton;      // Tham chiếu đến nút Next

    private float currentPage = 0;
    private float totalPages;
    private float pageWidth;

    void Start()
    {
        // Đảm bảo các button đã được gán
        if (nextButton != null)
            nextButton.onClick.AddListener(ScrollToNextPage);

        totalPages = scrollRect.content.childCount;
        pageWidth = 1 / (totalPages - 2);
    }

    void ScrollToNextPage()
    {
        if (currentPage < totalPages - 2)
        {
            currentPage++;
            ScrollToPage(currentPage);
        }
    }

    void ScrollToPage(float pageIndex)
    {
        float targetPosition = pageIndex * pageWidth;
        StartCoroutine(SmoothScrollTo(targetPosition));
    }

    System.Collections.IEnumerator SmoothScrollTo(float targetPosition)
    {
        float startPosition = scrollRect.horizontalNormalizedPosition;
        float elapsedTime = 0f;
        float duration = 0.15f;  // Thời gian cuộn (giây)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
    }
}
