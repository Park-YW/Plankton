using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForgeUI : MonoBehaviour
{
    public TextMeshProUGUI[] inventext;
    public Forge _forge;
    public bool active = false;

    private void Awake()
    {
        _forge = FindObjectOfType<Forge>();
    }
    void Start()
    {
        gameObject.SetActive(active);

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
        gameObject.SetActive(active && _forge._isEntering);
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
