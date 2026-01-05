using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("이동하는 데 걸리는 시간 (초)")]
    public float transitionDuration = 1.5f;

    [Tooltip("움직임의 느낌")]
    public Ease movementEase = Ease.InOutCubic;

    public void MoveToTarget(Transform target)
    {
        // 이전에 실행 중이던 트윈이 있다면 즉시 중단
        transform.DOKill();

        // 위치 이동
        transform.DOMove(target.position, transitionDuration)
            .SetEase(movementEase);

        // 회전 이동
        // 3D 공간에서 카메라는 짐벌락 방지를 위해 Quaternion 사용
        transform.DORotateQuaternion(target.rotation, transitionDuration)
            .SetEase(movementEase);
    }
}