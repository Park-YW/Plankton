using UnityEngine;

public class LaserDevice : MonoBehaviour
{
    public float laserLength = 10f; // 총 레이저 길이
    public Color laserColor = Color.red; // 레이저 색상
    public float laserWidth = 0.1f; // 레이저 두께
    public LayerMask blockLayer; // 감지할 레이어
    public int maxReflections = 5; // 최대 반사 횟수

    public GameObject laserReceiver; // 레이저 리시버 객체 (씬에서 할당)

    private LineRenderer lineRenderer;

    void Start()
    {
        // LineRenderer 초기화
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 2D용 기본 셰이더
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
    }

    void Update()
    {
        // 레이저의 시작점과 방향 계산
        Vector3 startPoint = transform.position;
        Vector3 direction = transform.right;

        // 레이저 경로 업데이트
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

                // 레이저 리시버 객체와 충돌 확인
                if (hitObject == laserReceiver)
                {
                    Debug.Log("레이저 수신: " + hitObject.name);

                    // 레이저 리시버 색상 변경
                    SpriteRenderer receiverRenderer = hitObject.GetComponent<SpriteRenderer>();
                    if (receiverRenderer != null)
                    {
                        receiverRenderer.color = Color.yellow;
                    }

                    // 레이저 종료
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hitPoint);
                    break;
                }

                if (hitObject.name == "B_Titanium(Clone)")
                {
                    // 티타늄 블록에 반사
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
                    // 블록 파괴
                    Destroy(hitObject);
                    switch (hitObject.tag)
                    {
                        case "Iron":
                            ResourceManager.Instance.AddItem("철", 1);
                            Debug.Log("철 자원 획득");
                            break;
                        case "Wood":
                            ResourceManager.Instance.AddItem("나무", 1);
                            Debug.Log("나무 자원 획득");
                            break;
                        case "Steel":
                            ResourceManager.Instance.AddItem("고무", 1);
                            Debug.Log("고무 자원 획득");
                            break;
                        case "Titanium":
                            ResourceManager.Instance.AddItem("티타늄", 1);
                            Debug.Log("티타늄 자원 획득");
                            break;
                        case "Stone":
                            ResourceManager.Instance.AddItem("돌", 1);
                            Debug.Log("돌 자원 획득");
                            break;
                        case "Dirt":
                            ResourceManager.Instance.AddItem("흙", 1);
                            Debug.Log("흙 자원 획득");
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
