using UnityEngine;

public class EventManager: MonoBehaviour
{
    public GameObject detector;  // ������ ������Ʈ ����
    public GameObject blockPrefab;  // ������ ��� ������ ����
    public LayerMask groundLayer;  // ���̳� �ٸ� ����� �����ϱ� ���� ���̾� ����ũ

    void Update()
    {
        // ���콺 ��ġ�� �����ɴϴ�.
        Vector3 mousePosition = Input.mousePosition;

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�մϴ�.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���콺�� Z ��ǥ�� 0���� �����Ͽ� 2D ������ ����ϴ�.
        worldPosition.z = 0;

        // ������ ������Ʈ�� ��ġ�� ���콺 ��ġ�� ������Ʈ�մϴ�.
        detector.transform.position = worldPosition;

        // ��Ŭ�� �� ��� ����
        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ�� (1�� ��Ŭ�� ��ư)
        {
            // ������ ��ġ���� �浹 ����
            Collider2D hit = Physics2D.OverlapPoint(worldPosition, groundLayer);

            // �浹�� ��ü�� ������ ��� ����
            if (hit == null)
            {
                Instantiate(blockPrefab, worldPosition, Quaternion.identity);
            }
        }
    }
}
