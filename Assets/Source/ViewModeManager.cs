using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class ViewModeManager : MonoBehaviour
{
    [System.Serializable]
    public class ViewOption
    {
        public string viewName;
        public Button button;          // 클릭할 버튼 본체

        [Header("UI Components")]
        public Image buttonBackground; // 버튼의 배경 이미지 (Button 오브젝트 본체)
        public Image buttonIcon;       // 버튼 안에 들어있는 아이콘 이미지 (자식)

        [Header("Camera Settings")]
        public Transform cameraTarget;
        public float fieldOfView = 60f;
    }

    [Header("Configuration")]
    public List<ViewOption> viewList;

    [Header("References")]
    public CameraController cameraController;
    public PaletteController paletteController;

    [Header("Color Style")]
    // 선택되었을 때 (Active)
    public Color activeBgColor = Color.black;      // 배경: 검정
    public Color activeIconColor = Color.white;    // 아이콘: 흰색

    // 선택 안 되었을 때 (Inactive)
    public Color inactiveBgColor = Color.white;    // 배경: 흰색 (또는 투명)
    public Color inactiveIconColor = Color.black;  // 아이콘: 검정

    private void Start()
    {
        foreach (var view in viewList)
        {
            var currentView = view;
            if (currentView.button != null)
            {
                // 버튼 클릭 시 SelectView 실행
                currentView.button.onClick.AddListener(() => SelectView(currentView));
            }
        }

        // 시작하자마자 첫 번째 뷰 선택 상태로 만들기
        if (viewList.Count > 0) SelectView(viewList[0]);
    }

    public void SelectView(ViewOption selectedOption)
    {
        foreach (var view in viewList)
        {
            bool isSelected = (view == selectedOption);

            Color targetBg = isSelected ? activeBgColor : inactiveBgColor;
            Color targetIcon = isSelected ? activeIconColor : inactiveIconColor;

            if (view.buttonBackground != null)
            {
                view.buttonBackground.DOColor(targetBg, 0.2f);
            }

            if (view.buttonIcon != null)
            {
                view.buttonIcon.DOColor(targetIcon, 0.2f);
            }
        }

        // 카메라 이동 로직
        if (cameraController != null && selectedOption.cameraTarget != null)
        {
            cameraController.MoveToTarget(selectedOption.cameraTarget);
            Camera.main.DOFieldOfView(selectedOption.fieldOfView, cameraController.transitionDuration)
                 .SetEase(cameraController.movementEase);
        }

        // 팔레트 컨트롤러에게 모드 전달
        if (paletteController != null)
        {
            bool isExterior = (selectedOption.viewName == "Exterior");
            paletteController.SetViewMode(isExterior);
        }
    }
}