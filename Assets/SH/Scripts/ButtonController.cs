using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor; // ��ư�� ����� ��
    private bool isPlayerInRange = false;
    private bool isButtonPressed = false;

    void Update()
    {
            if (isButtonPressed)
            {
                connectedDoor.OpenDoor(); // ���� ���� �Լ� ȣ��
            }
            else
            {
                connectedDoor.CloseDoor(); // ��ư�� ������ �ʾ��� �� ���� ����
            }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("BlockLayer"))
        {
            isButtonPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("BlockLayer"))
        {
            isButtonPressed=false;
        }
    }
}
