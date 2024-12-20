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
        temp.needs.Add("µ¹", 10);
        temp.needs.Add("³ª¹«", 20);
        temp.make = "µå¸±ÆÈ";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("Ã¶", 10);
        temp.needs.Add("Èë", 30);
        temp.needs.Add("°í¹«", 10);
        temp.make = "ºÎ½ºÅÍ";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("Ã¶", 20);
        temp.needs.Add("µ¹", 20);
        temp.needs.Add("³ª¹«", 10);
        temp.make = "µå¸±ÆÈ+";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("Ã¶", 30);
        temp.needs.Add("Å¸Å¸´½", 5);
        temp.needs.Add("°í¹«", 30);
        temp.make = "Á¦Æ®ÆÑ";
        ListToMake.Add(temp);
        temp = new CraftingList();
        temp.needs.Add("¿À·¡µÈ Åé´Ï¹ÙÄû", 1);
        temp.needs.Add("¿À·¡µÈ ±â°èÁ¶°¢", 1);
        temp.needs.Add("³ì½¼ Åé´Ï¹ÙÄû", 1);
        temp.needs.Add("³ì½¼ ·¹¹ö", 1);
        temp.make = "¿¤·¹º£ÀÌÅÍ";
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
