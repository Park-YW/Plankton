using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public GameObject blockPrefab; // ������ ��� ������
    public Camera mainCamera;      // �� ī�޶�
    public LayerMask backgroundLayer; // ��� ���̾� ����ũ
    public bool ableToSpawn;
    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���Ǿ����� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlockIfBackgroundOnly();
        }
    }

    void SpawnBlockIfBackgroundOnly()
    {
        // ���콺 ��ġ�� �������� Ray ���� (2D ȯ��)
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        bool backgroundHit = false;
        bool otherObjectHit = false;

        // Raycast2D�� �浹�� ��� ������Ʈ �˻�
        foreach (RaycastHit2D hit in hits)
        {
            if (((1 << hit.collider.gameObject.layer) & backgroundLayer) != 0)
            {
                backgroundHit = true;
            }
            else
            {
                otherObjectHit = true;
            }
        }

        // ��游 �浹���� ��쿡�� ��� ����
        if (backgroundHit && !otherObjectHit && ableToSpawn)
        {
            Instantiate(blockPrefab, mousePosition, Quaternion.identity);
            Debug.Log("��� ���� ����!");
        }
        else
        {
            Debug.Log("��� �̿��� ������Ʈ�� �����Ͽ� ��� �������� ����.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 ) //BackGroundLayer�� 3��
        {
            ableToSpawn = true;
        }
        else
        {
            ableToSpawn = false;
        }
    }
}