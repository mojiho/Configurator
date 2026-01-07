/*
 Global Enum 관리 파일
*/


/// <summary>
/// 자동차 모델 종류
/// </summary>
public enum CarTrim
{
    STD,        // 스탠다드
    XLINE,      // X-Line
}

/// <summary>
/// 교체 가능한 부품의 종류
/// </summary>
public enum PartType
{
    None,       // 없음 (메뉴 닫힘)
    Paint,      // [외장 기본] 차체 색상
    Wheel,      // [외장 옵션] 휠 교체
    Interior,   // [내장 기본] 내장재 색상
    RoofRack,   // [외장 옵션] 루프랙
    Exterior,   // [외장 옵션] 기타 외관 파츠
    MaytonAll   // Mayton 파츠 전체 
}

/// <summary>
/// 카메라 뷰 상태
/// </summary>
public enum CameraView
{
    Exterior,   // 외관 보기
    Interior,   // 실내 보기
    WheelDetail // 휠 확대 보기
}
