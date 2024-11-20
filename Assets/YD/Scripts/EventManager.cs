using UnityEngine;

public class EventManager: MonoBehaviour
{
    public GameObject detector;  // 감지기 오브젝트 참조
    public GameObject blockPrefab;  // 생성할 블록 프리팹 참조
    public LayerMask groundLayer;  // 땅이나 다른 블록을 감지하기 위한 레이어 마스크

    void Update()
    {
        // 마우스 위치를 가져옵니다.
        Vector3 mousePosition = Input.mousePosition;

        // 마우스 위치를 월드 좌표로 변환합니다.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 마우스의 Z 좌표를 0으로 설정하여 2D 공간에 맞춥니다.
        worldPosition.z = 0;

        // 감지기 오브젝트의 위치를 마우스 위치로 업데이트합니다.
        detector.transform.position = worldPosition;

        // 우클릭 시 블록 생성
        if (Input.GetMouseButtonDown(1)) // 마우스 우클릭 (1은 우클릭 버튼)
        {
            // 감지기 위치에서 충돌 감지
            Collider2D hit = Physics2D.OverlapPoint(worldPosition, groundLayer);

            // 충돌된 객체가 없으면 블록 생성
            if (hit == null)
            {
                Instantiate(blockPrefab, worldPosition, Quaternion.identity);
            }
        }
    }
}
