using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

// 부품 선택 UI 관리를 담당하는 스크립트

public class PartSelectionManager : MonoBehaviour
{
    public CarConfigDatabase database;
    public Transform buttonContainer;
    public GameObject buttonPrefab;

    public void RefreshInventory(List<PartType> typesToShow)
    {
        // 기존 UI 아이템 제거
        foreach (Transform child in buttonContainer) Destroy(child.gameObject);

        // 모든 요청된 타입의 데이터를 수집
        List<CarPartData> combinedData = new List<CarPartData>();
        foreach (var type in typesToShow)
        {
            combinedData.AddRange(database.GetDataList(type));
        }

        // 버튼 생성 및 데이터 바인딩
        foreach (var data in combinedData)
        {
            GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
            SetupButton(btnObj.GetComponent<Button>(), data);
        }
    }

    private void SetupButton(Button btn, CarPartData data)
    {
        // 아이콘 및 텍스트 설정
        Image icon = btn.GetComponent<Image>();
        if (data.icon != null) icon.sprite = data.icon;
        else if (data.material != null) icon.color = data.material.color;

        // 클릭 시 실제 장착 로직 실행
        btn.onClick.AddListener(() => {
            ExecuteApply(data);
            UpdateSelectionUI(btn);
        });
    }

    private void ExecuteApply(CarPartData data)
    {
        // 데이터 타입에 따라 알맞은 매니저 함수 호출
        if (data is CarPaintData paint) CarConfiguratorManager.Instance.ApplyPaint(paint);
        else if (data is CarModelData model) CarConfiguratorManager.Instance.ApplyWheel(model);

        // 상품 배너 클릭 시 카메라 이동 연동 
        // CameraTargetFocus(data.focusTransform); 
    }

    private void UpdateSelectionUI(Button selected)
    {
        foreach (Transform child in buttonContainer)
        {
            child.DOScale(child.gameObject == selected.gameObject ? 1.2f : 1.0f, 0.2f);
        }
    }
}