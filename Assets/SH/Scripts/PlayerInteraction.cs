using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask resourceLayer; //�ڿ��� ���� �� �ִ� ���̾� ����ũ
    public LayerMask chestLayer; // ���� ���̾� ����ũ
    public float interactionHoldTime = 1.0f; // �ڿ��� ��� ���� E Ű�� ������ �ϴ� �ð�
    private Vector3 savePoint; // ����� ���̺� ��ġ

    private float holdTime = 0.0f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // 'E' Ű�� ���ڿ� ��ȣ�ۿ�
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
                    holdTime = 0.0f; // �ڿ��� ȹ���ϰ� ���� holdTime�� �ʱ�ȭ
                }
        }
        else
        {
            holdTime = 0.0f;
        }
    }

    void CheckForResourceInteraction()
    {
        // �÷��̾� �ֺ��� �ڿ��� ��ȣ�ۿ��ϱ� ���� Physics2D ������ ����
        Collider2D[] hitResources = Physics2D.OverlapCircleAll(transform.position, 1.0f, resourceLayer);

        foreach (Collider2D resource in hitResources)
        {
            switch (resource.tag)
            {
                case "Iron":
                    ResourceManager.Instance.AddItem("ö", 1);
                    Debug.Log("ö �ڿ� ȹ��");
                    break;
                case "Wood":
                    ResourceManager.Instance.AddItem("����", 1);
                    Debug.Log("���� �ڿ� ȹ��");
                    break;
                case "Steel":
                    ResourceManager.Instance.AddItem("��", 1);
                    Debug.Log("�� �ڿ� ȹ��");
                    break;
                case "Titanium":
                    ResourceManager.Instance.AddItem("ƼŸ��", 1);
                    Debug.Log("ƼŸ�� �ڿ� ȹ��");
                    break;
                case "Stone":
                    ResourceManager.Instance.AddItem("��", 1);
                    Debug.Log("�� �ڿ� ȹ��");
                    break;
                case "Dirt":
                    ResourceManager.Instance.AddItem("��", 1);
                    Debug.Log("�� �ڿ� ȹ��");
                    break;
                // �ʿ信 ���� �ٸ� �ڿ� ó�� �߰� ����
                default:
                    Debug.LogWarning("�� �� ���� �ڿ�: " + resource.tag);
                    break;

            }
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

    public void SetSavePoint(Vector3 position)
    {
        savePoint = position; // ����� ���̺� ��ġ ����
        Debug.Log("���̺� ����Ʈ ���ŵ�: " + savePoint);
    }

    public void RespawnAtSavePoint()
    {
        if (savePoint != null)
        {
            transform.position = savePoint; // ����� ��ġ�� �̵�
            Debug.Log("�÷��̾ ���̺� ����Ʈ�� �̵��߽��ϴ�: " + savePoint);
        }
        else
        {
            Debug.LogWarning("���̺� ����Ʈ�� �������� �ʾҽ��ϴ�!");
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
