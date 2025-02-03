using UnityEngine;
using UnityEngine.UI;

public class AdjustUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private float targetAspectRatio;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetAspectRatio = 2400f / 1080f; // Fixed 스크립트에서 설정한 해상도 비율
    }

    void Update()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;

        if (currentAspectRatio < targetAspectRatio)
        {
            // 화면 비율이 목표 비율보다 작은 경우 (세로로 긴 화면)
            rectTransform.anchorMin = new Vector2(0f, (1f - targetAspectRatio / currentAspectRatio) / 2f);
            rectTransform.anchorMax = new Vector2(1f, 1f - (1f - targetAspectRatio / currentAspectRatio) / 2f);
        }
        else
        {
            // 화면 비율이 목표 비율보다 큰 경우 (가로로 긴 화면)
            rectTransform.anchorMin = new Vector2((1f - currentAspectRatio / targetAspectRatio) / 2f, 0f);
            rectTransform.anchorMax = new Vector2(1f - (1f - currentAspectRatio / targetAspectRatio) / 2f, 1f);
        }
    }
}