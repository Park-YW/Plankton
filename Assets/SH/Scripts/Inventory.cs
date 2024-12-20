using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Playables;
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

    public Sprite[] imageList;
    public GameObject[] ETCList;
    private string[] etcled = { "������ ��Ϲ���", "������ �������", "�콼 ��Ϲ���", "�콼 ����" };
    public Color[] basicBlockColors;

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
        for (int i = 3; i >= ForgeList.Length; i--)
        {
            TextMeshProUGUI text = ForgeList[i].GetComponentInChildren<TextMeshProUGUI>();
            text.text = "0";
            text.color = Color.white;
        }

        for (int i = 0; i < ETCList.Length; i++)
        {
            TextMeshProUGUI text = ETCList[i].GetComponentInChildren<TextMeshProUGUI>();
            text.text = ResourceManager.Instance.GetResourceAmount(etcled[i]).ToString();
            Debug.Log(etcled[i]);
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

                image.sprite = imageList[_forge._currentNumber];
            }
            else
            {
                if (craftlist[i] == "��")
                {
                    image.color = HexToColor("A5663A");
                }
                if (craftlist[i] == "����")
                {
                    image.color = HexToColor("582F00");
                }
                if (craftlist[i] == "��")
                {
                    image.color = HexToColor("616161");
                }
                if (craftlist[i] == "ö")
                {
                    image.color = HexToColor("B1B1B1");
                }
                if (craftlist[i] == "��")
                {
                    image.color = HexToColor("3C6267");
                }
                if (craftlist[i] == "ƼŸ��")
                {
                    image.color = HexToColor("C1B844");
                }
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
        

    }
    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", ""); // "#" ���� ����
        if (hex.Length != 6)
        {
            Debug.LogError("��ȿ���� ���� Hex ���Դϴ�.");
            return Color.white;
        }

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, 255);
    }

}
