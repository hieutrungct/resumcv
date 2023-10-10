using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rubik_Casual.ScrollView
{
    public class ScrollViewScrolling : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public RectTransform content;
        public float targetScrollPosition; // Vị trí muốn cuộn đến
        public float duration = 1f; // Thời gian (s) để cuộn đến vị trí mới

        void Start()
        {
            // Gọi hàm cuộn ScrollView với hiệu ứng chậm rã trong khoảng thời gian duration
            SmoothScrollToPosition(targetScrollPosition, duration);
        }

        void SmoothScrollToPosition(float targetPosition, float duration)
        {
            // Tính toán vị trí cuối cùng
            float startPosition = content.anchoredPosition.y;
            float startTime = Time.time;
            float endTime = startTime + duration;

            // Bắt đầu cuộn ScrollView trong khoảng thời gian duration
            // Sử dụng Coroutine để thực hiện việc cuộn mượt mà
            StartCoroutine(ScrollCoroutine(startPosition, targetPosition, startTime, endTime, duration));
        }

        IEnumerator ScrollCoroutine(float start, float end, float startTime, float endTime, float duration)
        {
            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / duration;
                float newY = Mathf.Lerp(start, end, t);
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, newY);
                yield return null;
            }
            // Đảm bảo cuộn đến vị trí cuối cùng
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, end);
        }
    }
}
