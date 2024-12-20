using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject invenUI;
    public TextMeshProUGUI[] inventext;
    public GameObject amountSlide;
    public Forge _forge;
    public GameObject ForgeUI;
    public GameObject[] ForgeList;
    public bool active = false;

    public Image imageList;

    private void Awake()
    {
        _forge = FindObjectOfType<Forge>();
    }
    void Start()
    {
        invenUI.SetActive(active);
        ForgeUI.SetActive(active);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            ToggleInven(active);

        }
        if (_forge.isCraft)
        {
            _forge.isCraft = false;
            UpdateInven();
        }
        amountSlide.GetComponent<Slider>().value = ResourceManager.Instance.GetTotalAmount();
    }

    public void ToggleInven(bool active)
    {
        invenUI.SetActive(active);
        ForgeUI.SetActive(active && _forge._isEntering);
        UpdateInven();
    }

    public void UpdateInven()
    {
        int tempCounter = 0;
        foreach (Item item in ResourceManager.Instance.inven)
        {
            inventext[tempCounter].text = item.itemQuantity.ToString();
            tempCounter++;
        }

        List<string> craftlist = _forge.GetCraftList();
        for (int i = 0; i < ForgeList.Length; i++)
        {
            TextMeshProUGUI text = ForgeList[i].GetComponentInChildren<TextMeshProUGUI>();
            Image image = ForgeList[i].GetComponent<Image>();
            if (i == 0)
            {
                text.text = "1";
                if (ResourceManager.Instance.GetResourceAmount(craftlist[i]) <=0)
                {
                    text.color = Color.red;
                }
                else
                {
                    text.color = Color.green;
                }


            }
            else
            {
                text.text = _forge.ListToMake[_forge._currentNumber].needs[craftlist[i]].ToString();
                if (ResourceManager.Instance.GetResourceAmount(craftlist[i]) < _forge.ListToMake[_forge._currentNumber].needs[craftlist[i]])
                {
                    text.color = Color.red;
                }
                else
                {
                    text.color = Color.green;
                }
            }


            
            
        }
        for (int i = 3;i >= ForgeList.Length; i--)
        {
            TextMeshProUGUI text = ForgeList[i].GetComponentInChildren<TextMeshProUGUI>();
            text.text = "0";
            text.color = Color.green;
        }

    }

}
