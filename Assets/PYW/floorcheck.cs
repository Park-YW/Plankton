using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Floorcheck : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isFloorTouch = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    { 
        if (collision.CompareTag("Block"))
        {
            Debug.Log("f");
            isFloorTouch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Debug.Log("f");
            isFloorTouch = false;
        }
    }
}
