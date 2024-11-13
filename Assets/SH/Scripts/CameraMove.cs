using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float smoothSpeed = 0.125f; // 카메라 이동 속도
    public Vector2 deadZoneSize = new Vector2(2f, 2f); // 데드존 크기 (가로, 세로)

    private Vector3 cameraOffset; // 초기 카메라와 플레이어의 거리

    void Start()
    {
        // 초기 카메라와 플레이어의 거리 계산
        cameraOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + cameraOffset; // 카메라가 따라가야 할 위치
        Vector3 cameraPosition = transform.position;

        // 플레이어가 데드존을 벗어났는지 확인
        if (Mathf.Abs(targetPosition.x - cameraPosition.x) > deadZoneSize.x / 2 ||
            Mathf.Abs(targetPosition.y - cameraPosition.y) > deadZoneSize.y / 2)
        {
            // Lerp를 사용해 카메라가 천천히 목표 위치로 이동
            Vector3 smoothedPosition = Vector3.Lerp(cameraPosition, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
