using UnityEngine;

public class ResponsiveWebGL : MonoBehaviour
{
    [Header("PC용 UI (가로형)")]
    public GameObject landscapeUI;

    [Header("모바일용 UI (세로형)")]
    public GameObject portraitUI;

    // 이전 상태를 기억해서, 화면이 바뀔 때만 코드를 실행
    private bool isLandscape = true;

    void Start()
    {
        // 시작할 때 한 번 체크
        CheckOrientation(true);
    }

    void Update()
    {
        // WebGL은 브라우저 크기를 실시간으로 줄일 수 있으므로 계속 감시해야 함
        CheckOrientation(false);
    }

    void CheckOrientation(bool forceUpdate)
    {
        // 가로가 세로보다 길면 -> 가로형(PC/태블릿)
        bool currentIsLandscape = Screen.width > Screen.height;

        // 상태가 바뀌었거나 강제 업데이트일 경우에만 UI 교체
        if (currentIsLandscape != isLandscape || forceUpdate)
        {
            isLandscape = currentIsLandscape;

            if (isLandscape)    // 가로모드로 전환
            {
                landscapeUI.SetActive(true);
                portraitUI.SetActive(false);
            }
            else               // 세로모드로 전환
            {
                landscapeUI.SetActive(false);
                portraitUI.SetActive(true);
            }
        }
    }
}