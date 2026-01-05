using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/*토글 버튼을 이용한 슬라이딩 탭 UI 스크립트입니다.*/
public class SlidingTabUI : MonoBehaviour
{
    [Header("---- 필수 연결 ----")]
    [Tooltip("움직일 이미지")]
    public RectTransform activeIndicator;

    [Tooltip("제어할 토글 버튼들 (순서대로)")]
    public Toggle[] toggles;

    [Header("---- 디자인 설정 ----")]
    public Color textSelectedColor = Color.white;   // 선택됐을 때 글자색
    public Color textUnselectedColor = Color.black; // 꺼졌을 때 글자색
    public float animDuration = 0.3f;               // 애니메이션 속도
    public Ease moveEase = Ease.OutBack;            // 이동할 때 느낌

    private void Start()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i; // 클로저 문제 해결용 임시 변수
            toggles[i].onValueChanged.AddListener((isOn) =>
            {
                if (isOn) UpdateUI(index, true);
            });
        }

        InitializeUI();
    }

    // 초기화 함수 (애니메이션 X)
    void InitializeUI()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                activeIndicator.anchoredPosition = toggles[i].GetComponent<RectTransform>().anchoredPosition;
                SetTextColor(toggles[i], textSelectedColor, 0f);
            }
            else
            {
                SetTextColor(toggles[i], textUnselectedColor, 0f);
            }
        }
    }

    // UI 업데이트 함수
    void UpdateUI(int targetIndex, bool animate)
    {
        RectTransform targetRect = toggles[targetIndex].GetComponent<RectTransform>();

        activeIndicator.DOAnchorPos(targetRect.anchoredPosition, animDuration);//.SetEase(moveEase);
        // activeIndicator.DOSizeDelta(targetRect.sizeDelta, animDuration).SetEase(moveEase); // 크기 다르다면 주석 해제

        for (int i = 0; i < toggles.Length; i++)
        {
            if (i == targetIndex)
            {
                SetTextColor(toggles[i], textSelectedColor, animDuration);
            }
            else
            {
                SetTextColor(toggles[i], textUnselectedColor, animDuration);
            }
        }
    }

    void SetTextColor(Toggle toggle, Color targetColor, float duration)
    {
        TextMeshProUGUI textComp = toggle.GetComponentInChildren<TextMeshProUGUI>();

        if (textComp != null)
        {
            if (duration > 0)
                textComp.DOColor(targetColor, duration); // 부드럽게 변경
            else
                textComp.color = targetColor; // 즉시 변경
        }
    }
}