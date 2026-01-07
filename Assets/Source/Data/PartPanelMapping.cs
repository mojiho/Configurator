using UnityEngine;

/*파츠 타입과 해당 파츠의 패널을 매핑하기 위한 클래스*/
[System.Serializable]
public class PartPanelMapping
{
    public PartType type;      
    public GameObject panel;
}