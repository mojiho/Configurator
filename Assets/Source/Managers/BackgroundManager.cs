using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

/*배경 변경 관리 스크립트입니다.*/

public class BackgroundManager : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundOption
    {
        public string bgName;          // 배경 이름 (예: Studio, City)
        public Button button;          // UI 버튼

        [Header("UI Styling")]
        public Image buttonBackground; // 버튼 배경 (검정/흰색 변할 곳)
        public Image buttonIcon;       // 버튼 아이콘/텍스트 (흰색/검정 변할 곳)
        // 만약 텍스트를 쓴다면 Text 컴포넌트도 추가해서 색을 바꾸면 됩니다.

        [Header("3D Scene")]
        public GameObject backgroundObject; // 씬에서 켜고 끌 실제 배경 모델 (부모 오브젝트)
    }

    [Header("Configuration")]
    public List<BackgroundOption> backgroundList;

    [Header("Color Style")]
    // 선택되었을 때 (Active)
    public Color activeBgColor = Color.black;
    public Color activeIconColor = Color.white;

    // 선택 안 되었을 때 (Inactive)
    public Color inactiveBgColor = Color.white;
    public Color inactiveIconColor = Color.black;

    private void Start()
    {
        foreach (var bg in backgroundList)
        {
            var currentBg = bg;
            if (currentBg.button != null)
            {
                currentBg.button.onClick.AddListener(() => SelectBackground(currentBg));
            }
        }

        // 시작 시 첫 번째 배경(보통 Studio) 자동 선택
        if (backgroundList.Count > 0)
        {
            SelectBackground(backgroundList[0]);
        }
    }

    public void SelectBackground(BackgroundOption selectedOption)
    {
        foreach (var bg in backgroundList)
        {
            bool isSelected = (bg == selectedOption);

            // 3D 배경 오브젝트 켜고 끄기
            if (bg.backgroundObject != null)
            {
                bg.backgroundObject.SetActive(isSelected);
            }

            // UI 색상 반전 (스타일링)
            Color targetBg = isSelected ? activeBgColor : inactiveBgColor;
            Color targetIcon = isSelected ? activeIconColor : inactiveIconColor;

            if (bg.buttonBackground != null)
                bg.buttonBackground.DOColor(targetBg, 0.2f);

            if (bg.buttonIcon != null)
                bg.buttonIcon.DOColor(targetIcon, 0.2f);
        }

        Debug.Log($"Background changed to: {selectedOption.bgName}");
    }
}