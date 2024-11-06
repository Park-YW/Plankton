using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floorcheck : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isFloorTouch;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isFloorTouch = true;
        }
        else
        {
            isFloorTouch = false;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isFloorTouch = false;
    }
}
