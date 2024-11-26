using UnityEngine;

public class Electric : MonoBehaviour
{
    public float electricRadius = 5f; // �������� �ݰ�, �������� ������ ������ �ݰ��� �����մϴ�.
    public GameObject electricFieldPrefab; // �������� ǥ���� ���� ������, �ð������� �������� ������ ��Ÿ���ϴ�.
    private GameObject electricFieldInstance; // �������� ǥ���ϴ� ���� �ν��Ͻ� ��ü�Դϴ�.
    public LayerMask blockLayer; // ��� ���̾� ����ũ, �������� Ȯ���� ��ϵ��� �����ϱ� ���� �����մϴ�.

    private void Start()
    {
        // ������ �������� �����ϰ� �������ϰ� �����մϴ�.
        if (electricFieldPrefab != null)
        {
            // �������� ElectricBlock ��ü�� ��ġ�� �����ϰ� �θ�� �����մϴ�.
            electricFieldInstance = Instantiate(electricFieldPrefab, transform.position, Quaternion.identity, transform);

            // �������� �ݰ濡 �°� ũ�� �����մϴ�.
            electricFieldInstance.transform.localScale = new Vector3(electricRadius * 2, electricRadius * 2, 1);

            // �������� SpriteRenderer�� ���� ������ �������ϰ� �����մϴ�.
            SpriteRenderer spriteRenderer = electricFieldInstance.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0.3f; // ���� ���� ���߾� �������ϰ� �����մϴ�.
                spriteRenderer.color = color;
            }

            // ������ �����տ� Collider2D �߰� �� ����
            if (electricFieldInstance.GetComponent<Collider2D>() == null)
            {
                CircleCollider2D circleCollider = electricFieldInstance.AddComponent<CircleCollider2D>();
                circleCollider.isTrigger = true;
                circleCollider.radius = electricRadius;
            }

            // ������ �����տ� ElectricFieldTrigger ��ũ��Ʈ�� �߰��Ͽ� �浹 ���� ó��
            ElectricFieldTrigger triggerScript = electricFieldInstance.AddComponent<ElectricFieldTrigger>();
            triggerScript.blockLayer = blockLayer;
            triggerScript.electricPrefab = electricFieldPrefab;
            triggerScript.electricRadius = electricRadius;
            triggerScript.blockLayer = blockLayer;
        }
    }

    private void Update()
    {
        // ������ ��ġ�� ElectricBlock�� �����ϰ� �����մϴ�.
        if (electricFieldInstance != null)
        {
            electricFieldInstance.transform.position = transform.position;
        }
    }
}

public class ElectricFieldTrigger : MonoBehaviour
{
    public LayerMask blockLayer; // ��� ���̾� ����ũ
    public GameObject electricPrefab; // �������� ǥ���� ���� ������
    public float electricRadius; // �������� �ݰ�

    private void OnTriggerStay2D(Collider2D collision)
    {
        // �浹�� ��ü�� ��� ���̾ ���� �ִ��� Ȯ���մϴ�.
        if (((1 << collision.gameObject.layer) & blockLayer) != 0)
        {
            Debug.Log("����� �����忡 �����Ͽ����ϴ�: " + collision.gameObject.name);

            // ��Ͽ� Electric ��ũ��Ʈ�� �߰��Ͽ� ���ο� �������� �����ϵ��� �մϴ�.
            Electric electricBlock = collision.gameObject.GetComponent<Electric>();
            if (electricBlock == null)
            {
                electricBlock = collision.gameObject.AddComponent<Electric>();
                electricBlock.electricRadius = electricRadius;
                electricBlock.electricFieldPrefab = electricPrefab;
                electricBlock.blockLayer = blockLayer;
            }
        }
    }
}
