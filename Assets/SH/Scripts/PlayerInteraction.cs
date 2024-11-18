using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask chestLayer; // ���� ���̾� ����ũ

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 'E' Ű�� ���ڿ� ��ȣ�ۿ�
        {
            Chest();
            Shop();
        }
    }

    private void Chest()
    {
        // �÷��̾� �ֺ��� ���ڿ� ��ȣ�ۿ��ϱ� ���� Physics2D ������ ����
        Collider2D[] hitChests = Physics2D.OverlapCircleAll(transform.position, 1.0f, chestLayer);

        foreach (Collider2D chest in hitChests)
        {
            Chest chestScript = chest.GetComponent<Chest>();
            if (chestScript != null && !chestScript.isCollected)
            {
                chestScript.CollectItem(); // ���� ������ ����
            }
        }
    }

    private void Shop()
    {

    }
}
