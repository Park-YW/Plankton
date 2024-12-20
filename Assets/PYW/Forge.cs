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
        dictemp.Add("µ¹", 10);
        dictemp.Add("³ª¹«", 20);
        CraftingList temp = new CraftingList("µå¸±ÆÈ", dictemp, "µå¸±ÆÈ ¼³°èµµ");
        ListToMake[0] = temp;
        dictemp = new Dictionary<string, int>();
        dictemp.Add("Ã¶", 10);
        dictemp.Add("Èë", 30);
        dictemp.Add("°í¹«", 10);
        temp = new CraftingList("ºÎ½ºÅÍ", dictemp, "ºÎ½ºÅÍ ¼³°èµµ");
        ListToMake[1] = temp;

        dictemp = new Dictionary<string, int>();
        dictemp.Add("Ã¶", 20);
        dictemp.Add("µ¹", 20);
        dictemp.Add("³ª¹«", 10);
        temp = new CraftingList("µå¸±ÆÈ+", dictemp, "µå¸±ÆÈ+ ¼³°èµµ");
        ListToMake[2] = temp;

        dictemp = new Dictionary<string, int>();
        dictemp.Add("Ã¶", 30);
        dictemp.Add("Æ¼Å¸´½", 5);
        dictemp.Add("°í¹«", 30);
        temp = new CraftingList("Á¦Æ®ÆÑ", dictemp, "Á¦Æ®ÆÑ ¼³°èµµ");
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
