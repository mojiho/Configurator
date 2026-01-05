using UnityEngine;
using System.Collections.Generic;

/*
    CarModelData
    전체 데이터를 관리용 스크립트
*/

[CreateAssetMenu(fileName = "ConfigDatabase", menuName = "Car Config/Database")]
public class CarConfigDatabase : ScriptableObject
{
    [Header("외장 색상 목록")]
    public List<CarPaintData> exteriorColors;

    [Header("휠 디자인 목록")]
    public List<CarModelData> wheels;

    [Header("내장 색상 목록")]
    public List<CarPaintData> interiorColors;
}