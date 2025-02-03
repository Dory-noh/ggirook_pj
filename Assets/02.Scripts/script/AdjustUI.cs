using UnityEngine;
using UnityEngine.UI;

public class AdjustUI : MonoBehaviour
{
    private RectTransform rectTransform;
    private float targetAspectRatio;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        targetAspectRatio = 2400f / 1080f; // Fixed ��ũ��Ʈ���� ������ �ػ� ����
    }

    void Update()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;

        if (currentAspectRatio < targetAspectRatio)
        {
            // ȭ�� ������ ��ǥ �������� ���� ��� (���η� �� ȭ��)
            rectTransform.anchorMin = new Vector2(0f, (1f - targetAspectRatio / currentAspectRatio) / 2f);
            rectTransform.anchorMax = new Vector2(1f, 1f - (1f - targetAspectRatio / currentAspectRatio) / 2f);
        }
        else
        {
            // ȭ�� ������ ��ǥ �������� ū ��� (���η� �� ȭ��)
            rectTransform.anchorMin = new Vector2((1f - currentAspectRatio / targetAspectRatio) / 2f, 0f);
            rectTransform.anchorMax = new Vector2(1f - (1f - currentAspectRatio / targetAspectRatio) / 2f, 1f);
        }
    }
}