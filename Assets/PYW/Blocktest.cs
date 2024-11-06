using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocktest : MonoBehaviour
{
    Rigidbody2D rb;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.drag = 10000;
        StartCoroutine(FallTimer());
    }
    IEnumerator FallTimer()
    {
        yield return new WaitForSeconds(3f);
        rb.drag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
