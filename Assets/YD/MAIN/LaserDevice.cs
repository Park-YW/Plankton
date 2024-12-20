using UnityEngine;

public class LaserDevice : MonoBehaviour
{
    public float laserLength = 10f; // �� ������ ����
    public Color laserColor = Color.red; // ������ ����
    public float laserWidth = 0.1f; // ������ �β�
    public LayerMask blockLayer; // ������ ���̾�
    public int maxReflections = 5; // �ִ� �ݻ� Ƚ��

    public GameObject laserReceiver; // ������ ���ù� ��ü (������ �Ҵ�)

    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer �ʱ�ȭ
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 2D�� �⺻ ���̴�
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
    }

    void Update()
    {
        // �������� �������� ���� ���
        Vector3 startPoint = transform.position;
        Vector3 direction = transform.right;

        // ������ ��� ������Ʈ
        UpdateLaserPath(startPoint, direction, laserLength);
    }

    private void UpdateLaserPath(Vector3 startPoint, Vector3 direction, float remainingLength)
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPoint);

        Vector3 currentPoint = startPoint;
        Vector3 currentDirection = direction;

        int reflectionCount = 0;

        while (remainingLength > 0f && reflectionCount < maxReflections)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPoint, currentDirection, remainingLength, blockLayer);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                Vector3 hitPoint = hit.point;

                // ������ ���ù� ��ü�� �浹 Ȯ��
                if (hitObject == laserReceiver)
                {
                    Debug.Log("������ ����: " + hitObject.name);

                    // ������ ���ù� ���� ����
                    SpriteRenderer receiverRenderer = hitObject.GetComponent<SpriteRenderer>();
                    if (receiverRenderer != null)
                    {
                        receiverRenderer.color = Color.yellow;
                    }

                    // ������ ����
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitPoint);
                    break;
                }

                if (hitObject.name == "B_Titanium(Clone)")
                {
                    // ƼŸ�� ��Ͽ� �ݻ�
                    Vector3 normal = hit.normal;
                    Vector3 reflectionDirection = Vector3.Reflect(currentDirection, normal);

                    float distanceToHit = Vector3.Distance(currentPoint, hitPoint);
                    remainingLength -= distanceToHit;

                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitPoint);

                    currentPoint = hitPoint;
                    currentDirection = reflectionDirection;

                    reflectionCount++;
                }
                else
                {
                    // ��� �ı�
                    Destroy(hitObject);
                    switch (hitObject.tag)
                    {
                        case "Iron":
                            ResourceManager.Instance.AddItem("ö", 1);
                            Debug.Log("ö �ڿ� ȹ��");
                            break;
                        case "Wood":
                            ResourceManager.Instance.AddItem("����", 1);
                            Debug.Log("���� �ڿ� ȹ��");
                            break;
                        case "Steel":
                            ResourceManager.Instance.AddItem("��", 1);
                            Debug.Log("�� �ڿ� ȹ��");
                            break;
                        case "Titanium":
                            ResourceManager.Instance.AddItem("ƼŸ��", 1);
                            Debug.Log("ƼŸ�� �ڿ� ȹ��");
                            break;
                        case "Stone":
                            ResourceManager.Instance.AddItem("��", 1);
                            Debug.Log("�� �ڿ� ȹ��");
                            break;
                        case "Dirt":
                            ResourceManager.Instance.AddItem("��", 1);
                            Debug.Log("�� �ڿ� ȹ��");
                            break;
                    }
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitPoint);
                    break;
                }
            }
            else
            {
                Vector3 endPoint = currentPoint + currentDirection * remainingLength;
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, endPoint);
                break;
            }
        }
    }
}
