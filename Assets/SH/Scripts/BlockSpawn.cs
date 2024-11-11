using System;
using UnityEngine;

public class BlockSpawn : MonoBehaviour
{
    public GameObject[] blockPrefabs; // 생성할 블록 프리팹들 0번 흙, 1번 나무, 2번 돌, 3번 철, 4번 강철, 5번 티타늄
    public Camera mainCamera;      // 주 카메라
    public LayerMask backgroundLayer, blockLayer; // 배경 레이어 마스크
    public bool ableToSpawn;
    private string[] whatBlock = { "흙","나무","돌","철","강철","티타늄"}; //0번 흙, 1번 나무, 2번 돌, 3번 철, 4번 강철, 5번 티타늄
    private int index = 0;
    private GameObject blockToDelete;
    private bool ableToDelete = false;

    private void Start()
    {
        index = 0;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){ SpawnBlockIfBackgroundOnly(whatBlock[index]); } //마우스 좌클릭 시 블록 생성
        if (Input.GetMouseButtonDown(1) && ableToDelete && blockToDelete != null) { Debug.Log("우클릭"); DeleteBlock(); }
        ChangeBlock(); //스크롤을 통해서 블록 바꾸기
    }

    void ChangeBlock()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 스크롤 업일 경우 index 증가
        if (scroll > 0f)
        {
            index++;
            if (index >= whatBlock.Length) index = 0; // 마지막 인덱스에서 처음으로 순환
        }
        // 스크롤 다운일 경우 index 감소
        else if (scroll < 0f)
        {
            index--;
            if (index < 0) index = whatBlock.Length - 1; // 첫 인덱스에서 마지막으로 순환
        }

        //Debug.Log("현재 블록: " + whatBlock[index]);
    }

    void SpawnBlockIfBackgroundOnly(string block)
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
            if (ResourceManager.Instance.UseResource(block, 5)) //자원 5개를 사용하여 블록 하나를 만들 수 있음
            {
                Instantiate(blockPrefabs[index], mousePosition, Quaternion.identity);
                Debug.Log("블록 생성 성공!");
            }
        }
        else
        {
            Debug.Log("배경 이외의 오브젝트가 존재하여 블록 생성되지 않음.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // 배경 레이어를 감지하여 ableToSpawn 설정
        if (collision.gameObject.layer == 3) // BackGroundLayer가 3번
        {
            ableToSpawn = true;
        }
        else
        {
            ableToSpawn = false;
        }
        if(((1<<collision.gameObject.layer) & blockLayer) != 0)
        {
            ableToDelete = true;
            blockToDelete = collision.gameObject;
        }
        else
        {
            ableToDelete = false;
            blockToDelete = null;
        }
    }

    private void DeleteBlock()
    {
        string blockName = blockToDelete.name.Replace("(Clone)", "").Trim(); // "프리팹이름(Clone)"에서 "(Clone)" 제거
        int blockIndex = Array.FindIndex(blockPrefabs, prefab => prefab.name == blockName);

        if (blockIndex >= 0 && blockIndex < whatBlock.Length)
        {
            ResourceManager.Instance.AddResource(whatBlock[blockIndex], 5); // 자원 반환
            Destroy(blockToDelete); // 블록 삭제
            Debug.Log("블록 삭제 및 자원 반환 성공!");
        }
        else
        {
            Debug.LogWarning("블록 프리팹에 존재하지 않는 블록입니다.");
        }

        ableToDelete = false;
        blockToDelete = null;
    }

}