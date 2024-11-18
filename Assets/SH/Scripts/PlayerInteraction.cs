using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask chestLayer; // 상자 레이어 마스크

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 'E' 키로 상자와 상호작용
        {
            Chest();
            Shop();
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
}
