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
    Paint,      // 외장 페인트
    Wheel,      // 휠
    Interior,   // 내장재
    RoofRack,    // 루프랙
    Exterior    // 외관
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
