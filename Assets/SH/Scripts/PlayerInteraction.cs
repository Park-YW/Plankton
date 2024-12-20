using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask resourceLayer; //자원을 얻을 수 있는 레이어 마스크
    public LayerMask chestLayer; // 상자 레이어 마스크
    public float interactionHoldTime = 1.0f; // 자원을 얻기 위해 E 키를 눌러야 하는 시간
    private Vector3 savePoint; // 저장된 세이브 위치

    private float holdTime = 0.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 'E' 키로 상자와 상호작용
        {
            Chest();
            Shop();
        }
        if (Input.GetKey(KeyCode.E))
        {
                holdTime += Time.deltaTime;
                if (holdTime >= interactionHoldTime)
                {
                    CheckForResourceInteraction();
                    holdTime = 0.0f; // 자원을 획득하고 나면 holdTime을 초기화
                }
        }
        else
        {
            holdTime = 0.0f;
        }
    }

    void CheckForResourceInteraction()
    {
        // 플레이어 주변의 자원과 상호작용하기 위해 Physics2D 오버랩 검출
        Collider2D[] hitResources = Physics2D.OverlapCircleAll(transform.position, 1.0f, resourceLayer);

        foreach (Collider2D resource in hitResources)
        {
            switch (resource.tag)
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
                // 필요에 따라 다른 자원 처리 추가 가능
                default:
                    Debug.LogWarning("알 수 없는 자원: " + resource.tag);
                    break;

            }
        }
    }

    private void Chest()
    {
        // 플레이어 주변의 상자와 상호작용하기 위해 Physics2D 오버랩 검출
        Collider2D[] hitChests = Physics2D.OverlapCircleAll(transform.position, 1.0f, chestLayer);

        foreach (Collider2D chest in hitChests)
        {
            Chest chestScript = chest.GetComponent<Chest>();
            if (chestScript != null && !chestScript.isCollected)
            {
                chestScript.CollectItem(); // 상자 아이템 수집
            }
        }
    }

    private void Shop()
    {

    }

    public void SetSavePoint(Vector3 position)
    {
        savePoint = position; // 저장된 세이브 위치 갱신
        Debug.Log("세이브 포인트 갱신됨: " + savePoint);
    }

    public void RespawnAtSavePoint()
    {
        if (savePoint != null)
        {
            transform.position = savePoint; // 저장된 위치로 이동
            Debug.Log("플레이어가 세이브 포인트로 이동했습니다: " + savePoint);
        }
        else
        {
            Debug.LogWarning("세이브 포인트가 설정되지 않았습니다!");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            RespawnAtSavePoint();
        }
    }
}
