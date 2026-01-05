using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

/* 차량의 특정 부위 색상을 변경하는 매니저 스크립트입니다. */

public class ColorChangeManager : MonoBehaviour
{
    [Header("Data Source")]
    public CarConfigDatabase database;
    public PartType partType;         

    [Header("UI Binding")]
    public List<Button> uiButtons;

    [Header("Target Settings")]
    public List<MeshRenderer> targetRenderers;
    public int materialIndex = 0;

    [Header("UI Feedback")]
    public float selectScale = 1.2f;

    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (database == null) return;

        var dataList = database.GetDataList(partType);
        if (dataList == null || dataList.Count == 0) return;

        int count = Mathf.Min(uiButtons.Count, dataList.Count);

        for (int i = 0; i < count; i++)
        {
            int index = i;
            Button btn = uiButtons[index];
            CarPartData data = dataList[index]; // 리스트에서 바로 데이터 꺼내옴

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnColorSelected(btn, data));

            // 아이콘 자동 적용: 이미지가 있으면 넣기
            // if (data.icon != null && btn.image != null) btn.image.sprite = data.icon;
        }

        for (int i = count; i < uiButtons.Count; i++)
        {
            uiButtons[i].gameObject.SetActive(false);
        }
    }

    public void OnColorSelected(Button selectedButton, CarPartData data)
    {
        ApplyMaterial(data.material);
        UpdateUI(selectedButton);

        //Debug.Log($"[{partType}] 선택됨: {data.partName} / 가격: {data.price}원");
    }

    private void ApplyMaterial(Material newMat)
    {
        if (newMat == null) return;
        foreach (var renderer in targetRenderers)
        {
            if (renderer == null) continue;
            Material[] mats = renderer.materials;
            if (mats.Length > materialIndex)
            {
                mats[materialIndex] = newMat;
                renderer.materials = mats;
            }
        }
    }

    private void UpdateUI(Button selectedButton)
    {
        foreach (var btn in uiButtons)
        {
            if (btn == null) continue;
            bool isSelected = (btn == selectedButton);
            float targetScale = isSelected ? selectScale : 1.0f;
            btn.transform.DOScale(targetScale, 0.3f).SetEase(Ease.OutBack);
        }
    }
}