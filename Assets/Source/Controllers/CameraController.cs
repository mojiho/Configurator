using UnityEngine;
using DG.Tweening;
/* 카메라 이동및 시점 전환 스크립트 입니다.*/
public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("이동하는 데 걸리는 시간 (초)")]
    public float transitionDuration = 1.5f;

    [Tooltip("움직임의 느낌")]
    public Ease movementEase = Ease.InOutCubic;

    // 궤도 컨트롤러 참조
    public CameraOrbitController orbitController;

    private void Start()
    {
        // 같은 오브젝트에 있으면 자동 연결
        if (orbitController == null)
            orbitController = GetComponent<CameraOrbitController>();
    }

    public void MoveToTarget(Transform target)
    {
        if (target == null) return;

        // 수동 조작 끄기
        if (orbitController) orbitController.SetControl(false);

        // 이동 및 회전
        Sequence seq = DOTween.Sequence();
        seq.Join(transform.DOMove(target.position, transitionDuration).SetEase(movementEase));
        seq.Join(transform.DORotateQuaternion(target.rotation, transitionDuration).SetEase(movementEase));

        // 이동 끝난 후
        seq.OnComplete(() =>
        {
            if (orbitController)
            {
                // 현재 위치 기준으로 궤도 재계산
                orbitController.SyncToCurrentCamera();
                // 수동 조작 다시 켜기
                orbitController.SetControl(true);
            }
        });

    }
}
