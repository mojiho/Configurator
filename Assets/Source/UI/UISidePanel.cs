using UnityEngine;
using DG.Tweening;

public class UISidePanel : MonoBehaviour
{
    [Header("설정")]
    public float animationDuration = 0.5f; // 열리는 데 걸리는 시간
    public Ease animationEase = Ease.OutExpo; // 연출 움직임 추가 OutBack, OutExpo 둘중에 하나가 제일 좋음

    [Header("위치 값 (RectTransform X좌표)")]
    public float hiddenX = 600f;  // 숨겨졌을 때 X 좌표 (패널 너비만큼)
    public float visibleX = 0f;   // 보일 때 X 좌표

    private RectTransform rectTransform;
    private bool isOpen = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        // 초기상태 애니메이션 없는 UI 위치 설정
        rectTransform.anchoredPosition = new Vector2(hiddenX, rectTransform.anchoredPosition.y);
        isOpen = false;
    }

    // 외부(버튼)에서 호출할 함수: 열기/닫기 토글
    public void TogglePanel()
    {
        if (isOpen) Close();
        else Open();
    }

    public void Open()
    {
        if (isOpen) return;

        // 실행 중이던 이전 애니메이션을 취소해서 꼬임을 방지함
        rectTransform.DOKill();
        rectTransform.DOAnchorPosX(visibleX, animationDuration).SetEase(animationEase);

        isOpen = true;
    }

    public void Close()
    {
        if (!isOpen) return;

        rectTransform.DOKill();
        rectTransform.DOAnchorPosX(hiddenX, animationDuration).SetEase(animationEase);

        isOpen = false;
    }
}