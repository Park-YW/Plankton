using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor; // 버튼과 연결된 문
    private bool isPlayerInRange = false;
    private bool isButtonPressed = false;

    void Update()
    {
            if (isButtonPressed)
            {
                connectedDoor.OpenDoor(); // 문을 여는 함수 호출
            }
            else
            {
                connectedDoor.CloseDoor(); // 버튼이 눌리지 않았을 때 문을 닫음
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
