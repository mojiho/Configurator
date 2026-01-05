using UnityEngine;
/*
    CarPartData
    기본 부품 데이터 스크립트
*/

public class CarPartData : ScriptableObject
{
    [Header("UI 표시 정보")]
    public string partName;      // 부품 이름 
    public int price;            // 가격
    public Sprite icon;          // UI 버튼에 들어갈 이미지

    [TextArea]
    public string description;   // 설명
}