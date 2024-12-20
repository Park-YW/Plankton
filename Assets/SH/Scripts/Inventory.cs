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
    public bool active = false;

    private void Awake()
    {
        _forge = FindObjectOfType<Forge>();
    }
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
        amountSlide.GetComponent<Slider>().value = ResourceManager.Instance.GetTotalAmount();
    }

    public void ToggleInven(bool active)
    {
        invenUI.SetActive(active);
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
    }

}
