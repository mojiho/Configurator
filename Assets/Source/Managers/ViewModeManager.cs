using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

/* 시점 전환 및 카메라 이동을 관리하는 매니저 스크립트 */
public class ViewModeManager : MonoBehaviour
{
    public static ViewModeManager Instance;

    [Header("Configuration")]
    public List<ViewOption> viewList;

    [Header("References")]
    public Camera mainCamera;
    public CameraOrbitController orbitController; // 카메라 조작 스크립트

    [Header("Color Style")]
    public Color activeBgColor = Color.black;
    public Color activeIconColor = Color.white;
    public Color inactiveBgColor = Color.white;
    public Color inactiveIconColor = Color.black;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 버튼 이벤트 자동 연결
        foreach (var view in viewList)
        {
            var currentView = view; // 클로저 캡처
            if (currentView.button != null)
            {
                currentView.button.onClick.AddListener(() => SelectView(currentView));
            }
        }

        // 첫 번째 뷰로 시작
        if (viewList.Count > 0) SelectView(viewList[0]);
    }

    public void SelectView(ViewOption selectedOption)
    {
        foreach (var view in viewList)
        {
            bool isSelected = (view == selectedOption);

            if (view.buttonBackground != null)
                view.buttonBackground.DOColor(isSelected ? activeBgColor : inactiveBgColor, 0.2f);

            if (view.buttonIcon != null)
                view.buttonIcon.DOColor(isSelected ? activeIconColor : inactiveIconColor, 0.2f);
        }

        // 2. ItemMenuController에게 시점 정보 전달 (새로운 구조 연동)
        if (ItemMenuController.Instance != null)
        {
            ItemMenuController.Instance.SetViewMode(selectedOption.isInterior);
        }

        // 3. 카메라 이동 연출 (DOTween)
        MoveCamera(selectedOption);
    }

    private void MoveCamera(ViewOption option)
    {
        if (mainCamera == null || orbitController == null) return;

        // 이동 중 조작 방지
        orbitController.enabled = false;

        Vector3 targetPos;
        Quaternion targetRot;

        if (option.isInterior)
        {
            targetPos = option.pivotPoint.position; // 눈의 위치가 곧 피벗
            targetRot = option.pivotPoint.rotation; // 피벗이 바라보는 방향
        }
        else
        {
            Vector3 offset = new Vector3(0, 2f, -option.distance);
            targetPos = option.pivotPoint.position + (option.pivotPoint.rotation * offset);
            targetRot = Quaternion.LookRotation(option.pivotPoint.position - targetPos);
        }

        // 시퀀스로 부드럽게 이동
        Sequence seq = DOTween.Sequence();

        // 이동 및 회전
        seq.Append(mainCamera.transform.DOMove(targetPos, 1.0f).SetEase(Ease.OutQuart));
        seq.Join(mainCamera.transform.DORotateQuaternion(targetRot, 1.0f).SetEase(Ease.OutQuart));
        seq.Join(mainCamera.DOFieldOfView(option.fieldOfView, 1.0f));

        // 이동 완료 후 OrbitController 설정 값 변경 및 재가동
        seq.OnComplete(() =>
        {
            orbitController.target = option.pivotPoint;
            orbitController.minDistance = option.isInterior ? 0.0f : 3.0f;
            orbitController.SyncToCurrentCamera();
            orbitController.enabled = true;
        });
    }
}