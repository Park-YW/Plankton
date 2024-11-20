using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject invenUI;
    public TextMeshProUGUI inventext;
    public bool active = false;
    void Start()
    {
        invenUI.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            ToggleInven(active);
        }
    }

    public void ToggleInven(bool active)
    {
        invenUI.SetActive(active);
        UpdateInven();
    }

    public void UpdateInven()
    {
        inventext.text = "";
        foreach (Item item in ResourceManager.Instance.inven)
        {
            inventext.text += $"{item.itemName} : {item.itemQuantity} || °ø°£: {item.itemSize * item.itemQuantity} || {item.itemType}\n";
        }
    }

}
