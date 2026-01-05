using UnityEngine;
/*
    CarPartData
    색상 변경용 데이터 (페인트, 가죽 등)
*/

[CreateAssetMenu(fileName = "New Paint Data", menuName = "Car Config/Paint Data")]
public class CarPaintData : CarPartData
{
    [Header("적용할 재질")]
    public Material material;  // 실제 차량에 입혀질 재질 마테리얼
}