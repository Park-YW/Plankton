using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float smoothTime = 0.3f; // 카메라가 목표에 도달하는 시간 (조정 가능)
    public Vector2 deadZoneSize = new Vector2(2f, 2f); // 데드존 크기

    private Vector3 velocity = Vector3.zero; // SmoothDamp에 사용할 현재 속도 저장

    void LateUpdate()
    {
        Vector3 targetPosition = player.position; // 플레이어의 위치
        Vector3 cameraPosition = transform.position;

        // 플레이어가 데드존을 벗어났는지 확인
        if (Mathf.Abs(targetPosition.x - cameraPosition.x) > deadZoneSize.x / 2 ||
            Mathf.Abs(targetPosition.y - cameraPosition.y) > deadZoneSize.y / 2)
        {
            // 플레이어가 멈춘 상태라면 카메라가 SmoothDamp로 따라옴
            if (!GameManager.Instance.isPlayerMoving)
            {
                Vector3 smoothedPosition = Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, smoothTime);
                transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, cameraPosition.z); // Z축 고정
            }
            else
            {
                // 플레이어가 오른쪽으로 이동 중일 때 x축을 조정
                float newX = cameraPosition.x;
                if (targetPosition.x > cameraPosition.x + deadZoneSize.x / 2)
                    newX = player.position.x - deadZoneSize.x / 2;
                else if (targetPosition.x < cameraPosition.x - deadZoneSize.x / 2)
                    newX = player.position.x + deadZoneSize.x / 2;

                // 플레이어가 위쪽으로 이동 중일 때 y축을 조정
                float newY = cameraPosition.y;
                if (targetPosition.y > cameraPosition.y + deadZoneSize.y / 2)
                    newY = player.position.y - deadZoneSize.y / 2;
                else if (targetPosition.y < cameraPosition.y - deadZoneSize.y / 2)
                    newY = player.position.y + deadZoneSize.y / 2;

                // 새로운 위치로 카메라 설정
                transform.position = new Vector3(newX, newY, cameraPosition.z);
            }
        }
    }
}