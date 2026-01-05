using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PaletteController : MonoBehaviour
{
    [Header("UI References")]
    public Button toggleButton;      // 우측에 있는 팔레트 여는 버튼
    public GameObject exteriorPanel; // 외장 색상 패널
    public GameObject interiorPanel; // 내장 색상 패널

    [Header("Animation Settings")]
    public float fadeTime = 0.3f;

    // 내부 상태 변수
    private bool isExpanded = false; // 현재 열려있는가?
    private bool isExteriorMode = true; // 현재 외관 모드인가?

    private void Start()
    {
        // 버튼 클릭 시 ToggleMenu 함수 실행
        if (toggleButton)
            toggleButton.onClick.AddListener(ToggleMenu);

        // 초기화: 패널들은 일단 다 끄고 시작
        if (exteriorPanel) exteriorPanel.SetActive(false);
        if (interiorPanel) interiorPanel.SetActive(false);
    }

    // 우측 버튼을 누르면 실행되는 함수
    public void ToggleMenu()
    {
        isExpanded = !isExpanded; // 켜짐<->꺼짐 상태 반전

        GameObject targetPanel = isExteriorMode ? exteriorPanel : interiorPanel;

        if (isExpanded)
        {
            // 열기: 패널을 켜고 애니메이션
            targetPanel.SetActive(true);

            // 투명도 0에서 1로 페이드인 효과
            CanvasGroup cg = targetPanel.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = 0;
                cg.DOFade(1f, fadeTime);
            }
        }
        else
        {
            // 닫기: 패널을 끔
            if (exteriorPanel) exteriorPanel.SetActive(false);
            if (interiorPanel) interiorPanel.SetActive(false);
        }
    }

    // ViewModeManager가 호출해줄 함수 (모드가 바뀔 때)
    public void SetViewMode(bool isExterior)
    {
        // 모드 상태 업데이트
        isExteriorMode = isExterior;

        isExpanded = false;
        if (exteriorPanel) exteriorPanel.SetActive(false);
        if (interiorPanel) interiorPanel.SetActive(false);
    }
}