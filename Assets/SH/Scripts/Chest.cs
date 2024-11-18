using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> items = new(); // 상자에 포함된 아이템 목록
    public bool isCollected = false; // 상자가 수집되었는지 여부

    public void CollectItem()
    {
        if (!isCollected)
        {
            bool allItemsCollected = true;
            foreach (Item item in items)
            {
                bool itemAdded = ResourceManager.Instance.AddItem(item.itemName, item.itemQuantity);
                if (!itemAdded)
                {
                    allItemsCollected = false;
                    Debug.LogWarning("인벤토리 공간 부족으로 아이템을 획득하지 못했습니다: " + item.itemName);
                }
                else
                {
                    Debug.Log("아이템 획득: " + item.itemName + " x" + item.itemQuantity);
                }
            }

            // 모든 아이템을 성공적으로 수집했을 경우 상자를 비활성화
            if (allItemsCollected)
            {
                isCollected = true;
                gameObject.SetActive(false); // 상자를 비활성화하여 수집 완료 표시
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 상호작용 범위를 표시하기 위한 기즈모 (디버깅 용도)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
