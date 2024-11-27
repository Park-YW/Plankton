using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor; // 버튼과 연결된 문
    private bool isPlayerInRange = false;
    private bool isButtonPressed = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            isButtonPressed = !isButtonPressed;
            if (isButtonPressed)
            {
                connectedDoor.OpenDoor(); // 문을 여는 함수 호출
            }
            else
            {
                connectedDoor.CloseDoor(); // 버튼이 눌리지 않았을 때 문을 닫음
            }
            Debug.Log("버튼 상태 변경: " + (isButtonPressed ? "눌림" : "해제"));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            isPlayerInRange = true; // 플레이어나 블록이 버튼 근처에 있을 때
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            isPlayerInRange = false; // 플레이어나 블록이 버튼에서 멀어질 때
            if (isButtonPressed)
            {
                connectedDoor.CloseDoor(); // 플레이어가 버튼을 떠났을 때 문을 닫음
                isButtonPressed = false;
            }
        }
    }
}
