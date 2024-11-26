using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CraftingList
{
    
}

public class Forge : MonoBehaviour
{
    public ForgePanel panel;
    bool _isEntering = false;

    private void Awake()
    {  
        panel = GetComponentInChildren<ForgePanel>();
        panel.gameObject.SetActive(false);
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
            panel.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out ff))
        {
            _isEntering = false;
            panel.gameObject.SetActive(false);
        }
    }
}
