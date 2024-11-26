using UnityEngine;

public class Electric : MonoBehaviour
{
    public float electricRadius = 5f; // 전기장의 반경, 전기장이 생성될 영역의 반경을 설정합니다.
    public GameObject electricFieldPrefab; // 전기장을 표시할 원형 프리팹, 시각적으로 전기장의 범위를 나타냅니다.
    private GameObject electricFieldInstance; // 전기장을 표시하는 실제 인스턴스 객체입니다.
    public LayerMask blockLayer; // 블록 레이어 마스크, 전기장을 확장할 블록들을 감지하기 위해 설정합니다.

    private void Start()
    {
        // 전기장 프리팹을 생성하고 반투명하게 설정합니다.
        if (electricFieldPrefab != null)
        {
            // 전기장을 ElectricBlock 객체의 위치에 생성하고 부모로 설정합니다.
            electricFieldInstance = Instantiate(electricFieldPrefab, transform.position, Quaternion.identity, transform);

            // 전기장의 반경에 맞게 크기 조정합니다.
            electricFieldInstance.transform.localScale = new Vector3(electricRadius * 2, electricRadius * 2, 1);

            // 전기장의 SpriteRenderer를 통해 색상을 반투명하게 설정합니다.
            SpriteRenderer spriteRenderer = electricFieldInstance.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0.3f; // 알파 값을 낮추어 반투명하게 설정합니다.
                spriteRenderer.color = color;
            }

            // 전기장 프리팹에 Collider2D 추가 및 설정
            if (electricFieldInstance.GetComponent<Collider2D>() == null)
            {
                CircleCollider2D circleCollider = electricFieldInstance.AddComponent<CircleCollider2D>();
                circleCollider.isTrigger = true;
                circleCollider.radius = electricRadius;
            }

            // 전기장 프리팹에 ElectricFieldTrigger 스크립트를 추가하여 충돌 감지 처리
            ElectricFieldTrigger triggerScript = electricFieldInstance.AddComponent<ElectricFieldTrigger>();
            triggerScript.blockLayer = blockLayer;
            triggerScript.electricPrefab = electricFieldPrefab;
            triggerScript.electricRadius = electricRadius;
            triggerScript.blockLayer = blockLayer;
        }
    }

    private void Update()
    {
        // 전기장 위치를 ElectricBlock과 동일하게 유지합니다.
        if (electricFieldInstance != null)
        {
            electricFieldInstance.transform.position = transform.position;
        }
    }
}

public class ElectricFieldTrigger : MonoBehaviour
{
    public LayerMask blockLayer; // 블록 레이어 마스크
    public GameObject electricPrefab; // 전기장을 표시할 원형 프리팹
    public float electricRadius; // 전기장의 반경

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 충돌한 객체가 블록 레이어에 속해 있는지 확인합니다.
        if (((1 << collision.gameObject.layer) & blockLayer) != 0)
        {
            Debug.Log("블록이 전기장에 접촉하였습니다: " + collision.gameObject.name);

            // 블록에 Electric 스크립트를 추가하여 새로운 전기장을 생성하도록 합니다.
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
