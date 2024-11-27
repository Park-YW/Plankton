using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false; // 문이 열렸는지 여부

    public void OpenDoor()
    {
        if (!isOpen)
        {
            // 문을 여는 로직 (애니메이션이나 상태 변경)
            isOpen = true;
            gameObject.SetActive(false); // 문을 비활성화하여 열리는 효과
            Debug.Log("문이 열렸습니다: " + gameObject.name);
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            // 문을 닫는 로직 (애니메이션이나 상태 변경)
            isOpen = false;
            gameObject.SetActive(true); // 문을 활성화하여 닫히는 효과
            Debug.Log("문이 닫혔습니다: " + gameObject.name);
        }
    }
}