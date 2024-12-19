using UnityEngine;

public class Save : MonoBehaviour
{
    private Vector3 savedPosition; // �÷��̾� ��ġ�� ������ ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") // �±׷� �÷��̾� Ȯ��
        {
            // �÷��̾��� ���� ��ġ�� ����
            savedPosition = collision.transform.position;
            Debug.Log("���̺� ��ġ ����: " + savedPosition);

            // �÷��̾��� PlayerMovement ������Ʈ�� ������ ���̺� ��ġ ����
            PlayerInteraction playerMovement = collision.GetComponent<PlayerInteraction>();
            if (playerMovement != null)
            {
                playerMovement.SetSavePoint(savedPosition); // ����� ��ġ ����
            }
            else
            {
                Debug.LogWarning("PlayerMovement ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
}
