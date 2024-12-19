using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor; // 버튼과 연결된 문
    private bool isPlayerInRange = false;
    private bool isButtonPressed = false;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("BlockLayer"))
        {
            connectedDoor.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("BlockLayer"))
        {
            connectedDoor.CloseDoor();
        }
    }
}
