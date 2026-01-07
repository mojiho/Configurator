using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ViewOption
{
    public string viewName;        // 에디터 식별용 이름
    public bool isInterior;        // 실내 뷰인가요?
    public Button button;          // 클릭할 버튼

    [Header("Camera Settings")]
    public Transform pivotPoint;   // 카메라가 바라볼 중심점 (Pivot)
    public float distance = 5.0f;  // 카메라와 피벗 사이 거리 (실내는 0 추천)

    [Range(20, 120)]
    public float fieldOfView = 60f;

    [Header("UI Styling (Optional)")]
    public Image buttonBackground;
    public Image buttonIcon;
}