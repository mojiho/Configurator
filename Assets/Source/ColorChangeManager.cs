using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

/*색상 변경 관리 스크립트입니다.*/

public class ColorChangeManager : MonoBehaviour
{
    [System.Serializable]
    public class ColorOption
    {
        public string colorName;       // 색상 이름 
        public Button button;          // 연결할 UI 버튼
        public Material material;      // 적용할 머티리얼
    }

    [Header("Color List")]
    public List<ColorOption> colorOptions; // 색상 버튼 목록

    [Header("Target Settings")]
    [Tooltip("색상을 바꿀 3D 오브젝트들 (예: 차체, 문짝 등)")]
    public List<MeshRenderer> targetRenderers;

    [Tooltip("해당 오브젝트에서 바꿀 재질의 순서 (0부터 시작)")]
    public int materialIndex = 0;

    [Header("UI Feedback")]
    public float selectScale = 1.2f; // 선택 시 버튼 커지는 비율

    private void Start()
    {
        foreach (var option in colorOptions)
        {
            var currentOption = option; // 클로저 문제 방지용 임시 변수
            if (currentOption.button != null)
            {
                currentOption.button.onClick.AddListener(() => OnColorSelected(currentOption));
            }
        }

        // (선택 사항) 시작할 때 첫 번째 색상을 자동으로 적용하려면 주석 해제
        // if (colorOptions.Count > 0) OnColorSelected(colorOptions[0]);
    }

    // 색상 버튼 클릭 시 실행
    public void OnColorSelected(ColorOption selectedOption)
    {
        // 실제 차량 색상 변경 (머티리얼 교체)
        ApplyMaterial(selectedOption.material);

        // UI 효과 (선택된 버튼만 커지고 나머지는 원래대로)
        UpdateUI(selectedOption);

        Debug.Log($"[{gameObject.name}] Color Changed to: {selectedOption.colorName}");
    }

    // 머티리얼 교체 로직
    private void ApplyMaterial(Material newMat)
    {
        if (newMat == null) return;

        foreach (var renderer in targetRenderers)
        {
            Material[] mats = renderer.materials; // 현재 재질 목록 가져오기

            // 인덱스가 유효한지 확인 후 교체
            if (mats.Length > materialIndex)
            {
                mats[materialIndex] = newMat;
                renderer.materials = mats; // 다시 적용해야 반영됨
            }
        }
    }

    // 버튼 크기 애니메이션
    private void UpdateUI(ColorOption selectedOption)
    {
        foreach (var option in colorOptions)
        {
            bool isSelected = (option == selectedOption);
            float targetScale = isSelected ? selectScale : 1.0f;

            if (option.button != null)
            {
                option.button.transform.DOScale(targetScale, 0.3f).SetEase(Ease.OutBack);
            }
        }
    }
}