using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false; // ���� ���ȴ��� ����

    public void OpenDoor()
    {
        if (!isOpen)
        {
            // ���� ���� ���� (�ִϸ��̼��̳� ���� ����)
            isOpen = true;
            gameObject.SetActive(false); // ���� ��Ȱ��ȭ�Ͽ� ������ ȿ��
            Debug.Log("���� ���Ƚ��ϴ�: " + gameObject.name);
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            // ���� �ݴ� ���� (�ִϸ��̼��̳� ���� ����)
            isOpen = false;
            gameObject.SetActive(true); // ���� Ȱ��ȭ�Ͽ� ������ ȿ��
            Debug.Log("���� �������ϴ�: " + gameObject.name);
        }
    }
}