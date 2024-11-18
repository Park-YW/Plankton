using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<Item> items = new(); // ���ڿ� ���Ե� ������ ���
    public bool isCollected = false; // ���ڰ� �����Ǿ����� ����

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
                    Debug.LogWarning("�κ��丮 ���� �������� �������� ȹ������ ���߽��ϴ�: " + item.itemName);
                }
                else
                {
                    Debug.Log("������ ȹ��: " + item.itemName + " x" + item.itemQuantity);
                }
            }

            // ��� �������� ���������� �������� ��� ���ڸ� ��Ȱ��ȭ
            if (allItemsCollected)
            {
                isCollected = true;
                gameObject.SetActive(false); // ���ڸ� ��Ȱ��ȭ�Ͽ� ���� �Ϸ� ǥ��
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ��ȣ�ۿ� ������ ǥ���ϱ� ���� ����� (����� �뵵)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.0f);
    }
}
