using System.Collections.Generic;
using UnityEngine;


public class ButtonDoorManager : MonoBehaviour
{
    public List<ButtonController> buttons; // ��ư ����Ʈ
    public List<DoorController> doors; // �� ����Ʈ

    private Dictionary<ButtonController, DoorController> buttonDoorMap;

    void Start()
    {
        // ��ư�� �� ���� ���� ����
        buttonDoorMap = new Dictionary<ButtonController, DoorController>();

        // ���÷� ��ư�� ���� �����ϴ� �ڵ�
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