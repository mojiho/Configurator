using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/* 카메라 무브먼트 컨트롤 스크립트 입니다.*/
public class CameraOrbitController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;

    [Header("Damping Settings (핵심)")]
    [Tooltip("값이 작을수록 더 늦게 따라옵니다 (쫀득한 느낌)")]
    public float rotationDamping = 5.0f;
    [Tooltip("값이 작을수록 줌이 부드럽게 멈춥니다")]
    public float zoomDamping = 5.0f;

    [Header("Sensitivity")]
    public float xSpeed = 5.0f;  // 마우스 회전 속도
    public float ySpeed = 5.0f;
    public float zoomSpeed = 0.5f; // 줌 속도

    [Header("Limits")]
    public float yMinLimit = -5f;
    public float yMaxLimit = 80f;
    public float minDistance = 3f;
    public float maxDistance = 9f;

    // 현재 카메라가 실제로 있는 위치 (부드럽게 변함)
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float currentDistance = 5.0f;

    // 마우스 입력으로 도달해야 할 목표 위치
    private float targetX = 0.0f;
    private float targetY = 0.0f;
    private float targetDistance = 5.0f;

    private bool isControllable = true;

    void Start()
    {
        // 초기화
        Vector3 angles = transform.eulerAngles;
        targetX = currentX = angles.y;
        targetY = currentY = angles.x;

        if (target != null)
        {
            currentDistance = targetDistance = Vector3.Distance(transform.position, target.position);
        }
    }

    void LateUpdate()
    {
        if (!target) return;

        // UI 위에 있는지 확인
        bool isOverUI = EventSystem.current.IsPointerOverGameObject();

        if (isControllable && Mouse.current != null && !isOverUI)
        {
            // 좌클릭 드래그 시 회전
            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 delta = Mouse.current.delta.ReadValue();
                targetX += delta.x * xSpeed * Time.deltaTime;
                targetY -= delta.y * ySpeed * Time.deltaTime;
                targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
            }

            // 휠 스크롤 시 거리 조절
            float scroll = Mouse.current.scroll.y.ReadValue() * 0.01f;
            targetDistance -= scroll * zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        }

        currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * rotationDamping);
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * rotationDamping);
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * zoomDamping);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -currentDistance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetControl(bool active)
    {
        isControllable = active;
    }
    public void SyncToCurrentCamera()
    {
        if (!target) return;

        // 현재 카메라가 타겟으로부터 얼마나 떨어져 있는지 계산
        float dist = Vector3.Distance(transform.position, target.position);
        currentDistance = targetDistance = dist;

        Vector3 direction = target.position - transform.position;

        // 그 방향을 바라보는 회전값(Quaternion)을 구합니다.
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 angles = lookRotation.eulerAngles;

        float angleX = angles.y; // Y축 회전 (좌우)
        float angleY = angles.x; // X축 회전 (상하)

        if (angleY > 180f) angleY -= 360f;
        if (angleX > 180f) angleX -= 360f;

        // 목표값과 현재값을 일치시켜 관성 초기화
        currentX = targetX = angleX;
        currentY = targetY = angleY;

        // 계산된 각도로 카메라 강제 정렬 (오차 제거)
        Quaternion finalRotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 finalPosition = finalRotation * new Vector3(0.0f, 0.0f, -currentDistance) + target.position;

        transform.rotation = finalRotation;
        transform.position = finalPosition;
    }
}