using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor; // ��ư�� ����� ��
    private bool isPlayerInRange = false;
    private bool isButtonPressed = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isButtonPressed = !isButtonPressed;
            if (isButtonPressed)
            {
                connectedDoor.OpenDoor(); // ���� ���� �Լ� ȣ��
            }
            else
            {
                connectedDoor.CloseDoor(); // ��ư�� ������ �ʾ��� �� ���� ����
            }
            Debug.Log("��ư ���� ����: " + (isButtonPressed ? "����" : "����"));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            isPlayerInRange = true; // �÷��̾ ����� ��ư ��ó�� ���� ��
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            isPlayerInRange = false; // �÷��̾ ����� ��ư���� �־��� ��
            if (isButtonPressed)
            {
                connectedDoor.CloseDoor(); // �÷��̾ ��ư�� ������ �� ���� ����
                isButtonPressed = false;
            }
        }
    }
}
