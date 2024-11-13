using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ�
    public Vector2 deadZoneSize = new Vector2(2f, 2f); // ������ ũ�� (����, ����)

    private Vector3 cameraOffset; // �ʱ� ī�޶�� �÷��̾��� �Ÿ�

    void Start()
    {
        // �ʱ� ī�޶�� �÷��̾��� �Ÿ� ���
        cameraOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + cameraOffset; // ī�޶� ���󰡾� �� ��ġ
        Vector3 cameraPosition = transform.position;

        // �÷��̾ �������� ������� Ȯ��
        if (Mathf.Abs(targetPosition.x - cameraPosition.x) > deadZoneSize.x / 2 ||
            Mathf.Abs(targetPosition.y - cameraPosition.y) > deadZoneSize.y / 2)
        {
            // Lerp�� ����� ī�޶� õõ�� ��ǥ ��ġ�� �̵�
            Vector3 smoothedPosition = Vector3.Lerp(cameraPosition, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
