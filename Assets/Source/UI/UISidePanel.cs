using UnityEngine;
using DG.Tweening;

/* 사이드 패널 조작을 위한 스크립트 입니다. */
public class UISidePanel : MonoBehaviour
{
    [Header("애니메이션 설정")]
    public float slideDuration = 0.5f;
    public Ease slideEase = Ease.OutExpo;

    [Header("위치 설정")]
    public float hiddenX = 600f;
    public float visibleX = 0f;

    private RectTransform rectTransform;
    public bool IsOpen { get; private set; } = false; // 하이라키에서 상태 확인

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(hiddenX, rectTransform.anchoredPosition.y);
        IsOpen = false;
    }

    public void Open()
    {
        if (IsOpen) return;

        rectTransform.DOKill();
        rectTransform.DOAnchorPosX(visibleX, slideDuration).SetEase(slideEase);
        IsOpen = true;
    }

    public void Close()
    {
        if (!IsOpen) return;

        rectTransform.DOKill();
        rectTransform.DOAnchorPosX(hiddenX, slideDuration).SetEase(slideEase);
        IsOpen = false;
    }
}