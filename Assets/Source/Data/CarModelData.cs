using UnityEngine;
/*
    CarModelData
    부품 교체용 데이터 스크립트
*/

[CreateAssetMenu(fileName = "New Model Data", menuName = "Car Config/Model Data")]
public class CarModelData : CarPartData
{
    [Header("교체할 모델")]
    public GameObject prefab;  // 교체될 3D 모델 (프리팹)
}