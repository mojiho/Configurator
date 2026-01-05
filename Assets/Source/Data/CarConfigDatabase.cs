using UnityEngine;
using System.Collections.Generic;


// [데이터베이스] 전체 차량 옵션 관리자
[CreateAssetMenu(fileName = "NewConfigDatabase", menuName = "Car Configurator/Config Database")]
public class CarConfigDatabase : ScriptableObject
{
    [Header("외장 색상 (Exterior)")]
    public List<CarPartData> exteriorColors;

    [Header("휠 옵션 (Wheels)")]
    public List<CarPartData> wheels;

    [Header("내장 색상 (Interior)")]
    public List<CarPartData> interiorColors;

    public List<CarPartData> GetDataList(PartType type)
    {
        switch (type)
        {
            case PartType.Exterior: return exteriorColors;
            case PartType.Wheel: return wheels;
            case PartType.Interior: return interiorColors;
            default: return null;
        }
    }
}