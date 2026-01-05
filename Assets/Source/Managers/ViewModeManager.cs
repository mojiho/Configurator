using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

/*뷰 모드(외관 / 내부) 전환 및 카메라 이동을 관리하는 매니저 스크립트입니다.*/

public class ViewModeManager : MonoBehaviour
{
    [System.Serializable]
    public class ViewOption
    {
        public string viewName;
        public Button button;

        [Header("Camera Settings")]
        public Transform cameraTarget; // 카메라가 도착할 위치
        public Transform orbitPivot;   

        [Range(20, 120)]
        public float fieldOfView = 60f;

        [Header("UI Styling")]
        public Image buttonBackground;
        public Image buttonIcon;

        [Header("Linked UI")]
        public GameObject colorUIPanel; 
    }

    [Header("Configuration")]
    public List<ViewOption> viewList;

    [Header("References")]
    public CameraController cameraController;
    public PaletteController paletteController;

    [Header("Color Style")]
    public Color activeBgColor = Color.black;
    public Color activeIconColor = Color.white;
    public Color inactiveBgColor = Color.white;
    public Color inactiveIconColor = Color.black;

    private void Start()
    {
        foreach (var view in viewList)
        {
            var currentView = view;
            if (currentView.button != null)
            {
                currentView.button.onClick.AddListener(() => SelectView(currentView));
            }
        }

        if (viewList.Count > 0) SelectView(viewList[0]);
    }

    public void SelectView(ViewOption selectedOption)
    {
        foreach (var view in viewList)
        {
            bool isSelected = (view == selectedOption);

            // 1. 버튼 스타일 변경
            if (view.buttonBackground != null)
                view.buttonBackground.DOColor(isSelected ? activeBgColor : inactiveBgColor, 0.2f);

            if (view.buttonIcon != null)
                view.buttonIcon.DOColor(isSelected ? activeIconColor : inactiveIconColor, 0.2f);

            // 2. 연결된 색상 패널(UI) 켜기/끄기
            if (view.colorUIPanel != null)
                view.colorUIPanel.SetActive(isSelected);
        }

        if (cameraController != null && selectedOption.cameraTarget != null)
        {
            // 오르빗 컨트롤러의 회전 중심축 설정
            if (cameraController.orbitController != null)
            {
                // 설정된 피벗이 있으면 그걸 쓰고, 없으면 그냥 자동차 중심(기존 타겟) 유지
                if (selectedOption.orbitPivot != null)
                    cameraController.orbitController.target = selectedOption.orbitPivot;
            }

            cameraController.MoveToTarget(selectedOption.cameraTarget);

            Camera.main.DOFieldOfView(selectedOption.fieldOfView, cameraController.transitionDuration)
                 .SetEase(cameraController.movementEase);
        }

        // 팔레트 상태 업데이트
        if (paletteController != null)
        {
            bool isExterior = (selectedOption.viewName == "Exterior");
            paletteController.SetViewMode(isExterior);
        }
    }
}