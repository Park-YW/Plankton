using System.Collections.Generic;
using UnityEngine;


public class ButtonDoorManager : MonoBehaviour
{
    public List<ButtonController> buttons; // 버튼 리스트
    public List<DoorController> doors; // 문 리스트

    private Dictionary<ButtonController, DoorController> buttonDoorMap;

    void Start()
    {
        // 버튼과 문 간의 관계 설정
        buttonDoorMap = new Dictionary<ButtonController, DoorController>();

        // 예시로 버튼과 문을 매핑하는 코드
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < doors.Count)
            {
                buttonDoorMap.Add(buttons[i], doors[i]);
            }
        }
    }

    public void OnButtonPressed(ButtonController button)
    {
        if (buttonDoorMap.ContainsKey(button))
        {
            DoorController door = buttonDoorMap[button];
            door.OpenDoor();
        }
    }
}