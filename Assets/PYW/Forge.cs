using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CraftingList
{
    public string make;
    public Dictionary<string, int> needs;
}
public enum ItemCodeList { Dirt, Wood, Rock, Iron, Rubber, Titanium, Gear, Lever }

public class Forge : MonoBehaviour
{
    public ForgePanel panel;
    public bool _isEntering = false;
    public List<CraftingList> ListToMake;
    int _currentNumber = 0;

    private void Awake()
    {  
        panel = GetComponentInChildren<ForgePanel>();
        panel.gameObject.SetActive(false);
    }

    private void Start()
    {
        CraftingList temp = new CraftingList();
        temp.needs.Add("��", 10);
        temp.needs.Add("����", 20);
        temp.make = "�帱��";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("ö", 10);
        temp.needs.Add("��", 30);
        temp.needs.Add("��", 10);
        temp.make = "�ν���";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("ö", 20);
        temp.needs.Add("��", 20);
        temp.needs.Add("����", 10);
        temp.make = "�帱��+";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("ö", 30);
        temp.needs.Add("ŸŸ��", 5);
        temp.needs.Add("��", 30);
        temp.make = "��Ʈ��";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("������ ��Ϲ���", 1);
        temp.needs.Add("������ �������", 1);
        temp.needs.Add("�콼 ��Ϲ���", 1);
        temp.needs.Add("�콼 ����", 1);
        temp.make = "����������";
        ListToMake.Add(temp);
    }


// Update is called once per frame
void Update()
    {

    }

    PlayerMovement ff;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out ff) && _isEntering)
        {
            panel.gameObject.transform.position = collision.transform.position + Vector3.up*1.2f;
            if (Input.GetKeyDown(KeyCode.F))
            {
                //collision.gameObject.GetComponent<PlayerInteraction>().testcode();
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out ff))
        {
            _isEntering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out ff))
        {
            _isEntering = false;
        }
    }
}
