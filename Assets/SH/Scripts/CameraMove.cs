using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float smoothTime = 0.3f; // ī�޶� ��ǥ�� �����ϴ� �ð� (���� ����)
    public Vector2 deadZoneSize = new Vector2(2f, 2f); // ������ ũ��

    private Vector3 velocity = Vector3.zero; // SmoothDamp�� ����� ���� �ӵ� ����

    void LateUpdate()
    {
        Vector3 targetPosition = player.position; // �÷��̾��� ��ġ
        Vector3 cameraPosition = transform.position;

        // �÷��̾ �������� ������� Ȯ��
        if (Mathf.Abs(targetPosition.x - cameraPosition.x) > deadZoneSize.x / 2 ||
            Mathf.Abs(targetPosition.y - cameraPosition.y) > deadZoneSize.y / 2)
        {
            // �÷��̾ ���� ���¶�� ī�޶� SmoothDamp�� �����
            if (!GameManager.Instance.isPlayerMoving)
            {
                Vector3 smoothedPosition = Vector3.SmoothDamp(cameraPosition, targetPosition, ref velocity, smoothTime);
                transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, cameraPosition.z); // Z�� ����
            }
            else
            {
                // �÷��̾ ���������� �̵� ���� �� x���� ����
                float newX = cameraPosition.x;
                if (targetPosition.x > cameraPosition.x + deadZoneSize.x / 2)
                    newX = player.position.x - deadZoneSize.x / 2;
                else if (targetPosition.x < cameraPosition.x - deadZoneSize.x / 2)
                    newX = player.position.x + deadZoneSize.x / 2;

                // �÷��̾ �������� �̵� ���� �� y���� ����
                float newY = cameraPosition.y;
                if (targetPosition.y > cameraPosition.y + deadZoneSize.y / 2)
                    newY = player.position.y - deadZoneSize.y / 2;
                else if (targetPosition.y < cameraPosition.y - deadZoneSize.y / 2)
                    newY = player.position.y + deadZoneSize.y / 2;

                // ���ο� ��ġ�� ī�޶� ����
                transform.position = new Vector3(newX, newY, cameraPosition.z);
            }
        }
    }
}