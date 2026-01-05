using UnityEngine;
/*
    UI 요소가 항상 카메라를 바라보도록 하는 스크립트
*/
public class UIBillboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // 시작시 메인 카메라 설정
    }

    void LateUpdate()
    {
        transform.forward = mainCamera.transform.forward;   // UI가 카메라를 바라보게
    }
}