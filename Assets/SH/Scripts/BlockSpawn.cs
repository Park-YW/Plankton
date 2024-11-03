using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public GameObject blockPrefab; // 생성할 블록 프리팹
    public Camera mainCamera;      // 주 카메라
    public LayerMask backgroundLayer; // 배경 레이어 마스크
    public bool ableToSpawn;
    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었는지 확인
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlockIfBackgroundOnly();
        }
    }

    void SpawnBlockIfBackgroundOnly()
    {
        // 마우스 위치를 기준으로 Ray 생성 (2D 환경)
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        bool backgroundHit = false;
        bool otherObjectHit = false;

        // Raycast2D로 충돌한 모든 오브젝트 검사
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

        // 배경만 충돌했을 경우에만 블록 생성
        if (backgroundHit && !otherObjectHit && ableToSpawn)
        {
            Instantiate(blockPrefab, mousePosition, Quaternion.identity);
            Debug.Log("블록 생성 성공!");
        }
        else
        {
            Debug.Log("배경 이외의 오브젝트가 존재하여 블록 생성되지 않음.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 ) //BackGroundLayer가 3번
        {
            ableToSpawn = true;
        }
        else
        {
            ableToSpawn = false;
        }
    }
}