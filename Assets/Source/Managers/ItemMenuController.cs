using UnityEngine;
using System.Collections.Generic;

/* 아이템 메뉴관리를 담당하는 스크립트 입니다. */
public class ItemMenuController : MonoBehaviour
{
    public static ItemMenuController Instance;

    [Header("필수 연결")]
    public UISidePanel sidePanel;

    [Header("패널 등록 (Inspector 설정)")]
    // PartPanelMapping이 별도 클래스라 바로 리스트로 사용 가능
    public List<PartPanelMapping> panelList;

    private Dictionary<PartType, GameObject> panelMap = new Dictionary<PartType, GameObject>();

    // 현재 시점 상태 (True: 실내 / False: 실외)
    private bool isInteriorView = false;

    // 현재 열려있는 파츠 타입
    public PartType CurrentPart { get; private set; } = PartType.None;

    private void Awake()
    {
        Instance = this;
        InitializePanels();
    }

    // 초기화 로직 분리
    private void InitializePanels()
    {
        foreach (var mapping in panelList)
        {
            if (mapping.panel != null && !panelMap.ContainsKey(mapping.type))
            {
                panelMap.Add(mapping.type, mapping.panel);
                mapping.panel.SetActive(false);
            }
        }
    }

    // =========================================================
    //  시점 변경 시 호출
    // =========================================================
    public void SetViewMode(bool isInterior)
    {
        isInteriorView = isInterior;

        // 메뉴가 열려있었다면, 바뀐 시점에 맞는 메뉴로 즉시 전환
        if (sidePanel.IsOpen)
        {
            OpenMainColorMenu();
        }
    }

    // =========================================================
    //  메뉴 열기/닫기 로직
    // =========================================================

    // 특정 파츠 메뉴 열기 (외부 호출용)
    public void OpenMenu(PartType type)
    {
        if (!panelMap.ContainsKey(type)) return;

        // 토글 기능: 이미 열려있고 같은 타입이면 닫기
        if (sidePanel.IsOpen && CurrentPart == type)
        {
            CloseMenu();
            return;
        }

        SwitchPanel(type);
    }

    // 현재 시점에 맞는 '기본 색상 메뉴' 열기
    public void OpenMainColorMenu()
    {
        PartType targetType = isInteriorView ? PartType.Interior : PartType.Paint;

        // 해당 패널이 없으면 무시
        if (!panelMap.ContainsKey(targetType)) return;

        SwitchPanel(targetType);
    }

    // [내부 함수] 패널 교체 및 애니메이션 실행 (중복 코드 제거)
    private void SwitchPanel(PartType type)
    {
        foreach (var panel in panelMap.Values) panel.SetActive(false);

        panelMap[type].SetActive(true);

        CurrentPart = type;
        sidePanel.Open();
    }

    public void CloseMenu()
    {
        sidePanel.Close();
    }

    // =========================================================
    //  UI 버튼 연결
    // =========================================================

    // [색상 변경 버튼] - 시점에 따라 Paint 혹은 Interior가 열림
    public void OnClickMainColor()
    {
        PartType targetType = isInteriorView ? PartType.Interior : PartType.Paint;

        if (sidePanel.IsOpen && CurrentPart == targetType)
        {
            CloseMenu();
        }
        else
        {
            OpenMainColorMenu();
        }
    }

    // [휠 변경 버튼]
    public void OnClickWheel() => OpenMenu(PartType.Wheel);

    // [핸들(화살표) 버튼]
    public void OnClickHandle()
    {
        if (sidePanel.IsOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMainColorMenu();
        }
    }
}