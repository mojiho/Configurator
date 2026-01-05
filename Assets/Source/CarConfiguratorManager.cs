using UnityEngine;
using System.Collections.Generic;
/*
    데이터를 읽어와서 차량의 색상과 아이템, 휠을 변경하는 매니저 스크립트입니다.
*/

public class CarConfiguratorManager : MonoBehaviour
{
    // 싱글톤
    public static CarConfiguratorManager Instance;

    [Header("데이터베이스 연결")]
    public CarConfigDatabase database; // 데이터베이스 스크립트 오브젝트

    [Header("차량 참조 (연결 필요)")]
    public MeshRenderer carBodyRenderer;   // 차체(Body) 오브젝트의 렌더러
    public Transform[] wheelMountPoints;   // 휠이 생성될 위치 4곳 (FL, FR, RL, RR)

    // 현재 장착된 휠들을 기억해두는 리스트 (삭제용)
    private List<GameObject> currentWheels = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 초기화 : 첫 번째 색상과 휠로 설정
        if (database.exteriorColors.Count > 0)
            ApplyPaint(database.exteriorColors[0]);

        if (database.wheels.Count > 0)
            ApplyWheel(database.wheels[0]);

        ApplyTrim(CarTrim.STD);
    }

    // =========================================================
    // 색상 변경 로직
    // =========================================================
    public void ApplyPaint(CarPaintData paintData)
    {
        if (carBodyRenderer)
        {
            carBodyRenderer.sharedMaterial = paintData.material;

            Debug.Log($"색상 변경 완료: {paintData.partName}");
        }
    }

    // UI 버튼에서 쉽게 호출하기 위한 함수
    public void SetColorByIndex(int index)
    {
        if (index >= 0 && index < database.exteriorColors.Count)
        {
            ApplyPaint(database.exteriorColors[index]);
        }
    }

    // =========================================================
    //  휠 교체 로직 (모델 변경)
    // =========================================================
    public void ApplyWheel(CarModelData wheelData)
    {
        // 기존에 달려있던 휠 삭제
        foreach (GameObject wheel in currentWheels)
        {
            if (wheel != null) Destroy(wheel);
        }
        currentWheels.Clear();

        // 바퀴 4개 위치에 새 휠 모델 생성
        foreach (Transform mountPoint in wheelMountPoints)
        {
            GameObject newWheel = Instantiate(wheelData.prefab, mountPoint.position, mountPoint.rotation, mountPoint);
            currentWheels.Add(newWheel);
        }

        //Debug.Log($"휠 교체 완료: {wheelData.partName}");
    }

    // UI 버튼용 함수
    public void SetWheelByIndex(int index)
    {
        if (index >= 0 && index < database.wheels.Count)
        {
            ApplyWheel(database.wheels[index]);
        }
    }


    // =========================================================
    //  트림 교체
    // =========================================================
    public void SetTrim(string trimName)
    {
        if (trimName == "STD")
        {
            Debug.Log("STD 트림 선택됨: 범퍼 교체, 전용 휠 장착...");
            // 여기에 STD용 범퍼 모델을 켜고, X-LINE용을 끄는 로직 추가
            // 예: bumperSTD.SetActive(true); bumperXLINE.SetActive(false);
        }
        else if (trimName == "XLINE")
        {
            Debug.Log("X-LINE 트림 선택됨: 전용 디자인 적용...");
            // 예: bumperSTD.SetActive(false); bumperXLINE.SetActive(true);
        }
    }

    public void ApplyTrim(CarTrim trimType)
    {
        // 초기화
        //foreach (var part in stdParts) if (part) part.SetActive(false);
        //foreach (var part in xlineParts) if (part) part.SetActive(false);

        // 선택된 트림에 맞는 파츠만 켭니다
        switch (trimType)
        {
            case CarTrim.STD:
                //foreach (var part in stdParts) if (part) part.SetActive(true);
                Debug.Log("트림 변경 완료: STD");
                break;

            case CarTrim.XLINE:
                //foreach (var part in xlineParts) if (part) part.SetActive(true);
                Debug.Log("트림 변경 완료: X-LINE");
                break;
        }
    }

    public void SetTrimFromToggle(int trimIndex)
    {
        // 숫자를 Enum으로 변환 (Casting)
        CarTrim selectedTrim = (CarTrim)trimIndex;

        // 실제 로직 실행
        ApplyTrim(selectedTrim);
    }
}
