using UnityEngine;
using UnityEngine.UI;

public class SlidePanelUI : MonoBehaviour
{
    [Header("슬라이드 설정")]
    public float slideSpeed = 10f;
    public float hiddenX = 400f; // 화면 밖으로 숨겨질 X 좌표 (패널 너비만큼)
    public float visibleX = 0f;  // 화면에 보일 때 X 좌표

    private RectTransform rectTransform;
    private bool isVisible = false;
    private float targetX;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // 시작할 때는 숨겨진 상태로 설정
        targetX = hiddenX;
        Vector2 pos = rectTransform.anchoredPosition;
        pos.x = hiddenX;
        rectTransform.anchoredPosition = pos;
    }

    void Update()
    {
        // 부드럽게 목표 위치로 이동 (Lerp)
        Vector2 currentPos = rectTransform.anchoredPosition;

        // 목표값과 현재값의 차이가 거의 없으면 연산 중단 (최적화)
        if (Mathf.Abs(currentPos.x - targetX) < 0.1f) return;

        float newX = Mathf.Lerp(currentPos.x, targetX, Time.deltaTime * slideSpeed);
        rectTransform.anchoredPosition = new Vector2(newX, currentPos.y);
    }

    // 외부(버튼)에서 호출할 함수
    public void TogglePanel()
    {
        isVisible = !isVisible;
        UpdateTargetPosition();
    }

    public void ShowPanel()
    {
        isVisible = true;
        UpdateTargetPosition();
    }

    public void HidePanel()
    {
        isVisible = false;
        UpdateTargetPosition();
    }

    private void UpdateTargetPosition()
    {
        targetX = isVisible ? visibleX : hiddenX;
    }
}