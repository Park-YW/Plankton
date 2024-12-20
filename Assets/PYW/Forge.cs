using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingList
{
    public string make;
    public Dictionary<string, int> needs;
    public string blueprint;
    public CraftingList(string make, Dictionary<string, int> needs, string blueprint)
    {
        this.make = make;
        this.needs = needs;
        this.blueprint = blueprint;
    }
}
public enum ItemCodeList { Dirt, Wood, Rock, Iron, Rubber, Titanium, Gear, Lever }

public class Forge : MonoBehaviour
{
    public ForgePanel panel;
    public bool _isEntering = false;
    public CraftingList[] ListToMake;
    int _currentNumber = 0;

    private void Awake()
    {  
        panel = GetComponentInChildren<ForgePanel>();
        panel.gameObject.SetActive(false);
        ListToMake = new CraftingList[5];
        Dictionary<string, int> dictemp = new Dictionary<string, int>();
        dictemp.Add("��", 10);
        dictemp.Add("����", 20);
        CraftingList temp = new CraftingList("�帱��", dictemp, "�帱�� ���赵");
        ListToMake[0] = temp;
        dictemp = new Dictionary<string, int>();
        dictemp.Add("ö", 10);
        dictemp.Add("��", 30);
        dictemp.Add("��", 10);
        temp = new CraftingList("�ν���", dictemp, "�ν��� ���赵");
        ListToMake[1] = temp;

        dictemp = new Dictionary<string, int>();
        dictemp.Add("ö", 20);
        dictemp.Add("��", 20);
        dictemp.Add("����", 10);
        temp = new CraftingList("�帱��+", dictemp, "�帱��+ ���赵");
        ListToMake[2] = temp;

        dictemp = new Dictionary<string, int>();
        dictemp.Add("ö", 30);
        dictemp.Add("ƼŸ��", 5);
        dictemp.Add("��", 30);
        temp = new CraftingList("��Ʈ��", dictemp, "��Ʈ�� ���赵");
        ListToMake[3] = temp;
    }

    private void Start()
    {
        
    }


// Update is called once per frame
    public void Craft()
    {

        ResourceManager.Instance.CraftItem(ListToMake[_currentNumber].blueprint, ListToMake[_currentNumber].needs, ListToMake[_currentNumber].make);
    }

    public void ChangeCraftList()
    {
        _currentNumber++;
        _currentNumber %= 4;
    }

    public List<string> GetCraftList()
    {
        List<string> list = new List<string>();
        list.Add(ListToMake[_currentNumber].blueprint);
        foreach(var need in ListToMake[_currentNumber].needs)
        {
            list.Add(need.Key);
        }

        return list;
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
